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
		MovementForward ();
		Rotation ();

		drone.AddRelativeForce (Vector3.up * upForce);
		drone.rotation = Quaternion.Euler(
			new Vector3(tiltAmount, currentYRotation, drone.rotation.z));
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
	public float tiltAmount = 0;
	public float tiltVelocity;

	void MovementForward() {
		if (Input.GetAxis ("Vertical") != 0) {
			drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardForce);
			tiltAmount = Mathf.SmoothDamp (tiltAmount, 20 * Input.GetAxis("Vertical"), ref tiltVelocity, 0.1f);
		}
	}

	public float wantedYRotation;
	public float currentYRotation;
	public float rotateAmountByKeys = 2.5f;
	public float rotationYVelocity;

	void Rotation() {
		if (Input.GetKey (KeyCode.J)) {
			wantedYRotation -= rotateAmountByKeys;
		}
		if (Input.GetKey (KeyCode.K)) {
			wantedYRotation += rotateAmountByKeys;
		}

		currentYRotation = Mathf.SmoothDamp (currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

	}
}
