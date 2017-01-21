using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int orbitingVerse;

	private Vector3 prevVel;

	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		Vector3 push = new Vector3 (10.0f, 0.0f, 0.0f);
		//body.AddForce (push);
		body.velocity = push;
	}

	void Update(){
		Rigidbody body = GetComponent<Rigidbody> ();

		Vector3 verse = new Vector3 (0.0f, 0.0f, 1.0f * orbitingVerse);
		if (orbitingVerse == -1) {
			// Orario
			transform.RotateAround (Vector3.zero, verse, 100 * Time.deltaTime);
		} else if (orbitingVerse == 1) {
			// Antiorario
			transform.RotateAround (Vector3.zero, verse, 100 * Time.deltaTime);
		} else if (orbitingVerse == 0) {
			body.velocity = prevVel;
		} else {
			prevVel = body.velocity;
		}

		if (orbitingVerse != 0) {
			// Debug.Log (body.velocity);
			if (Input.GetKey (KeyCode.Space)) {
				body.isKinematic = false;
//				Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(body.rotation.x, body.rotation.y, body.rotation.z), Vector3.one);
//				body.velocity = matrix.MultiplyPoint (transform.position) * 10;
				body.velocity = (Quaternion.Euler(0, 0, -90) * transform.position) * 5;
				orbitingVerse = 2;
			}
		}

	}

}
