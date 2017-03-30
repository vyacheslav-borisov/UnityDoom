using UnityEngine;
using System.Collections;

[AddComponentMenu("Control Scripts/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes
    {
        MouseXandY,
        MouseX,
        MouseY
    }

    public Transform playerTransform;

    public RotationAxes axis = RotationAxes.MouseXandY;
    public float sensivityHor = 9.0f;
    public float sensivityVert = 9.0f;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;
    private float _rotationY = 0;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body != null)
        {
            body.freezeRotation = true;
        }
    }

    void Update ()
    {
	    switch(axis)
        {
            case RotationAxes.MouseX:
                {
                    _rotationY += Input.GetAxis("Mouse X") * sensivityHor * Time.deltaTime;                    
                }
                break;
            case RotationAxes.MouseY:
                {
                    _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert * Time.deltaTime;
                    _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);                    
                }
                break;
            case RotationAxes.MouseXandY:
                {
                    _rotationX -= Input.GetAxis("Mouse Y") * sensivityVert * Time.deltaTime;
                    _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                    _rotationY += Input.GetAxis("Mouse X") * sensivityHor * Time.deltaTime;                    
                }
                break;
            default:
                break;
        }

        transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
        if (playerTransform != null)
        {
            playerTransform.localEulerAngles = new Vector3(0, _rotationY, 0);
        }
	}
}
