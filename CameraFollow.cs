using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform drone;

	void Awake() {
		drone = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	public Vector3 cameraVelocity;
	public Vector3 behindPosition = new Vector3(0,2,-4);
	public float cameraAngle = -10;

	void FixedUpdate() {
		transform.position = Vector3.SmoothDamp(transform.position, drone.transform.TransformPoint(behindPosition) + Vector3.up * Input.GetAxis("Vertical"), ref cameraVelocity, 0.1f);
		transform.rotation = Quaternion.Euler (new Vector3 (cameraAngle, drone.GetComponent<DroneMovement> ().currentYRotation, 0));
	}
}
