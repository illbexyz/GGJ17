using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public int orbitingVerse;

	private Vector3 prevVel;

	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		Vector3 push = new Vector3 (0.0f, 5.0f, 0.0f);
		body.AddForce (push * speed);
	}

	void Update(){
		Rigidbody body = GetComponent<Rigidbody> ();
		Vector3 push = new Vector3 (0.0f, 5.0f, 0.0f);

		Vector3 verse = new Vector3 (0.0f, 0.0f, 1.0f * orbitingVerse);
		if (orbitingVerse == -1) {
			transform.RotateAround (Vector3.zero, verse, 100 * Time.deltaTime);
		} else if (orbitingVerse == 1) {
			transform.RotateAround (Vector3.zero, verse, 100 * Time.deltaTime);
		} else if (orbitingVerse == 0) {
			body.velocity = prevVel;
		} else {
			prevVel = body.velocity;
		}

		if (orbitingVerse != 0) {
			Debug.Log (body.velocity);
			if (Input.GetKey (KeyCode.Space)) {
				
				orbitingVerse = 2;
			}
		}

	}
}
