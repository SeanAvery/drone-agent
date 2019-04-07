using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillRotation : MonoBehaviour {
	void FixedUpdate () {
		// rotate mill around ( ) axis
		transform.Rotate (2, 0, 0);
	}
}
