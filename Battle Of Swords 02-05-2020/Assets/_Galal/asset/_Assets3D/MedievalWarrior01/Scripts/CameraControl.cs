using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	private bool isPressed;
	private float speed =5;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	public Camera cam; 
	public float distance = 1.0f;
	private float panZ = 1.166f;
	float x = 0.0f;
	float y = 0.0f;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (1)) {
			x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
			

			Quaternion rotation = Quaternion.Euler (y, x, 0);

			transform.rotation = rotation;

		}
		distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*2, 0.5f, 10);
		cam.transform.localPosition = new Vector3(0,0.4f,-distance);

		if (Input.GetMouseButton (2)) {
			panZ -= Input.GetAxis ("Mouse Y") * 0.02f;
			transform.position = new Vector3(0,panZ,0);
		}

	}
}
