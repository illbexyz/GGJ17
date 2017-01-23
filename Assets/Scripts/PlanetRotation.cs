using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour {

	void Update () {
		Rigidbody body = GetComponent<Rigidbody> ();
		body.transform.Rotate(Vector3.right * Time.deltaTime * 10);
		body.transform.Rotate(Vector3.forward * Time.deltaTime * 10);
	}
}
