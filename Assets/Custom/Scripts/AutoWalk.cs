using UnityEngine;
using System.Collections;

public class AutoWalk : MonoBehaviour {

	[Tooltip ("Speed at which the player walks")]
	public float speed;

	[Tooltip ("Acceleration when player starts or stops walking")]
	public float acceleration;

	[Tooltip ("Gvr Viewer in the scene")]
	public GvrViewer viewer;

	private Camera headCamera;
	private bool walking;
	private float currentSpeed;

	// Use this for initialization
	void Start () {
		headCamera = this.GetComponentInChildren<Camera> ();
		walking = false;
	}

	private void Walk (float speed) {
		Vector3 xzTranslation = new Vector3 (headCamera.transform.forward.x, 0, headCamera.transform.forward.z);
		this.transform.position += xzTranslation.normalized * speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (viewer.Triggered) {
			walking = !walking;
		}

		if (walking) {
			if (currentSpeed < speed) {
				currentSpeed += acceleration * Time.deltaTime;
				if (currentSpeed > speed) {
					currentSpeed = speed;
				}
			}
			Walk (currentSpeed);
		} else {
			if (currentSpeed > 0) {
				currentSpeed -= acceleration * Time.deltaTime;
				if (currentSpeed < 0) {
					currentSpeed = 0;
				}
				Walk (currentSpeed);
			}
		}

	}
}
