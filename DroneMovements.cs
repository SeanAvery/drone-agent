using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour {
	Rigidbody drone;

	void Awake() {
		drone = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		MovementUpDown ();

		drone.AddRelativeForce (Vector3.up * upForce);
	}

	public float upForce;
	void MovementUpDown() {
		if (Input.GetKey (KeyCode.I)) {
			upForce = 400;
		} else if (Input.GetKey (KeyCode.K)) {
			upForce = -400;
		} else {
			upForce = 98.1f;
		}
	}
}
