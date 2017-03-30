using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float speed = 10.0f;
    public float damage = 1.0f; 

	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other) {
        FPSInput player = other.gameObject.GetComponent<FPSInput>();
        if(player != null)
        {
            Debug.Log("Player hit!");
        }

        Destroy(this.gameObject);
    }
}
