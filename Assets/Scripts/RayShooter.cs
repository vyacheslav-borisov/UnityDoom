using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class RayShooter : MonoBehaviour {

    private Camera _camera;

	// Use this for initialization
	void Start ()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2;
        float posY = _camera.pixelHeight / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");

        WanderingAI[] enemies = FindObjectsOfType<WanderingAI>();
        if(enemies.Length > 0)
        {
            Vector3 vDistance = enemies[0].transform.position - transform.position;
            GUI.Label(new Rect(_camera.pixelRect.xMin + 10, _camera.pixelRect.yMin + 10, 
                _camera.pixelRect.width * 0.2f, _camera.pixelHeight * 0.1f), "distance = " + vDistance.magnitude);            
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButton(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                GameObject go = hitInfo.transform.gameObject;
                ReactiveTarget target = go.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hitInfo.point));
                }
            }
        }
	}

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
