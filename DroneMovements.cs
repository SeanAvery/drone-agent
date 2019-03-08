using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovements : MonoBehaviour {
	Rigidbody drone;

	void Awake() {
		drone = GetComponent<Rigidbody>();
	}

	/*
	 * CONTROLS
	 */

	void FixedUpdate(){
		MovementUpDown();
		MovementForwardBack();
		Rotation();
		ClampingSpeedValues();
		MovementLeftRight();

		drone.AddRelativeForce(Vector3.up * upForce);
		drone.AddRelativeForce(Vector3.forward * forwardForce);
		drone.rotation = Quaternion.Euler(
			new Vector3(tiltAmount, currentYRotation, sideTiltAmount));
	}

	/*
	 * MOVEMENT UP
	 */

	public float upForce;
	void MovementUpDown() {

		// if the drone already has tilt, make adjustements
		if ((Mathf.Abs (Input.GetAxis ("Vertical")) > 0.2f || Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f)) {
			if (Input.GetKey (KeyCode.I) || Input.GetKey (KeyCode.K)) {
				// do nothing
			}

			// if forward/back and left/right are not being pressed
			if (!Input.GetKey(KeyCode.I) || !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)) {
				drone.velocity = new Vector3 (drone.velocity.x, Mathf.Lerp(drone.velocity.y, 0, Time.deltaTime * 5), drone.velocity.z);
				upForce = 2810;
			}

			// if moving left/right
			if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)) {
				drone.velocity = new Vector3 (drone.velocity.x, Mathf.Lerp(drone.velocity.y, 0, Time.deltaTime * 5), drone.velocity.z);
				upForce = 1100;
			}

		}


		if (Input.GetKey (KeyCode.I)) {
			upForce = 5000;
		} else if (Input.GetKey (KeyCode.K)) {
			upForce = -3000;
		} else {
			upForce = 980.1f;
		}
	}

	/*
	 * MOVEMENT FORWARD
	 */

	public float forwardForce;
	public float movementForwardSpeed = 5000.0f;
	public float movementBackwardSpeed = -5000.0f;
	public float tiltAmount = 0;
	public float tiltVelocity;

	void MovementForwardBack () {
		if (Input.GetKey (KeyCode.W)) {
			forwardForce = 5000;
			tiltAmount = Mathf.SmoothDamp (tiltAmount, 20.0f * Input.GetAxis ("Vertical"), ref tiltVelocity, 0.1f);
		} else if (Input.GetKey (KeyCode.S)) {
			forwardForce = -5000;
			tiltAmount = Mathf.SmoothDamp (tiltAmount, 20.0f * Input.GetAxis ("Vertical"), ref tiltVelocity, 0.1f);
		} else {
			forwardForce = 0;
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

//	public Vector3 velocityToZero;
	void ClampingSpeedValues() {
		drone.velocity = Vector3.ClampMagnitude (drone.velocity, Mathf.Lerp (drone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
	}

	/*
	 * MOVEMENT LEFT & RIGHT
	 */
	public float sideMovementAmount = 2500.0f;
	public float sideMovementAmount2 = -2500.0f;
	public float sideTiltAmount;
	public float sideTiltVelocity;

	void MovementLeftRight() {
		if (Input.GetKey (KeyCode.A)) {
			drone.AddRelativeForce (Vector3.right * Input.GetAxis ("Horizontal") * sideMovementAmount);
			sideTiltAmount = Mathf.SmoothDamp (sideTiltAmount, -20.0f * Input.GetAxis ("Horizontal"), ref sideTiltVelocity, 0.1f);
		} else if (Input.GetKey (KeyCode.D)) {
			drone.AddRelativeForce (Vector3.left * Input.GetAxis ("Horizontal") * sideMovementAmount2);
			sideTiltAmount = Mathf.SmoothDamp (sideTiltAmount, -20.0f * Input.GetAxis ("Horizontal"), ref sideTiltVelocity, 0.1f);
		}
	}

}
