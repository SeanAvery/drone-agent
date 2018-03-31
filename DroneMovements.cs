using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour {
	Rigidbody drone;

	void Awake() {
		drone = GetComponent<Rigidbody>();
	}

	/*
	 * CONTROLS
	 */

	void FixedUpdate(){
		MovementUpDown();
		MovementForward();
		Rotation();
		ClampingSpeedValues();

		drone.AddRelativeForce(Vector3.up * upForce);
		drone.rotation = Quaternion.Euler(
			new Vector3(tiltAmount, currentYRotation, drone.rotation.z));
	}

	/*
	 * MOVEMENT UP
	 */

	public float upForce;
	void MovementUpDown() {
		if (Input.GetKey (KeyCode.I)) {
			upForce = 1000;
		} else if (Input.GetKey (KeyCode.K)) {
			upForce = -1000;
		} else {
			upForce = 98.1f;
		}
	}

	/*
	 * MOVEMENT FORWARD
	 */

	public float movementForwardForce = 1000;
	public float tiltAmount = 0;
	public float tiltVelocity;

	void MovementForward() {
		if (Input.GetAxis ("Vertical") != 0) {
			drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardForce);
			tiltAmount = Mathf.SmoothDamp (tiltAmount, 20 * Input.GetAxis("Vertical"), ref tiltVelocity, 0.1f);
		}
	}

	/*
	 * ROTATION
	 */

	public float wantedYRotation;
	public float currentYRotation;
	public float rotateAmountByKeys = 2.5f;
	public float rotationYVelocity;

	void Rotation() {
		if (Input.GetKey (KeyCode.J)) {
			wantedYRotation -= rotateAmountByKeys;
		}
		if (Input.GetKey (KeyCode.L)) {
			wantedYRotation += rotateAmountByKeys;
		}

		currentYRotation = Mathf.SmoothDamp (currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);

	}

	/*
	 * FRICTION
	 */

	public Vector3 velocityToZero;

	void ClampingSpeedValues() {
		if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
			drone.velocity = Vector3.ClampMagnitude(drone.velocity, Mathf.Lerp(drone.velocity.magnitude, 10f), Time.deltaTime * 5f)
		}
	}

}
