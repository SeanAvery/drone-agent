using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotation : MonoBehaviour {
	void FixedUpdate() {
		if (Input.GetKey (KeyCode.I)) {
			transform.Rotate (0, 500, 0);
		} else {
			transform.Rotate (0, 100, 0);
		}
	}
}
