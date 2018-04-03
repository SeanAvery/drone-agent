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
		MovementLeftRight();

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
			upForce = 5000;
		} else if (Input.GetKey (KeyCode.K)) {
			upForce = -5000;
		} else {
			upForce = 98.1f;
		}
	}

	/*
	 * MOVEMENT FORWARD
	 */

	public float movementForwardForce = 0;
	public float tiltAmount = 0;
	public float tiltVelocity;

	void MovementForward() {
		if (Input.GetAxis ("Vertical") != 0) {
			drone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardForce*10);
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
	 * VELOCITY CAPING
	 */

	public Vector3 velocityToZero;

	void ClampingSpeedValues() {
		if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
			drone.velocity = Vector3.ClampMagnitude (drone.velocity, Mathf.Lerp (drone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
		}

//		if (Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f && Mathf.Abs (Input.GetAxis ("horizontal")) > 0.2f) {
//			drone.velocity = Vector3.ClampMagnitude(drone.velocity, Mathf.Lerp(drone.velocity.magnitude, 10.0f, Time.deltaTime * 5f))
//		}

	}

	/*
	 * MOVEMENT LEFT & RIGHT
	 */
	public float sideMovementAmount = 500.0f;
	public float sideTiltAmount;
	public float sideTiltVelocity;

	void MovementLeftRight() {
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f) {
			drone.AddRelativeForce (Vector3.right * Input.GetAxis ("horizontal") * sideMovementAmount);
			sideTiltAmount = Mathf.SmoothDamp (sideTiltAmount, 20.0f * Input.GetAxis ("Horizontal"), ref sideTiltVelocity, 0.1f);
		} else {
			sideTiltAmount = Mathf.SmoothDamp (sideTiltAmount, 0, ref sideTiltAmount, 0.1f);
		}
	}

}
