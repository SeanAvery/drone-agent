using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour {
	Rigidbody drone;

	void Awake() {
		drone = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		MovementUpDown ();
		MovementForward ();

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

	public float movementForwardForce = 500;
	public float tiltAmount;
	public float tiltVelocity;

	void MovementForward() {
		if (Input.GetAxis ("Vertical") != 0) {
			drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardForce) ;
		}
	}
}
