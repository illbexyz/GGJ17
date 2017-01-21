using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject controller;

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

			body.velocity = Vector3.zero;
			body.isKinematic = true;
			transform.RotateAround (Vector3.zero, verse, 100 * Time.deltaTime);
		} else if (orbitingVerse == 1) {
			// Antiorario

			body.velocity = Vector3.zero;
			body.isKinematic = true;
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

	void OnCollisionEnter(Collision c){
		Vector3 fwd = GetComponent<Rigidbody> ().transform.right;
		Vector3 point = c.contacts [0].point;
		Vector3 pos = GetComponent<Rigidbody> ().position;

		Vector3 normal = c.contacts[0].normal;
		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		float angleOne = Vector3.Angle (vel, -normal);


		Vector3 result = point - pos;
		float angle = Vector3.Angle (result, fwd);
					
		float signedAngle = SignedAngleBetween (result, fwd, transform.position.normalized);
		Debug.Log (angle);
		if (signedAngle > 0) {
			transform.Rotate (new Vector3(0f, 0f, 1f) * (180+angleOne));
			orbitingVerse = 1;
		} else if (signedAngle < 0) {
			orbitingVerse = -1;
			transform.Rotate (new Vector3(0f, 0f, -1f) * (180+angleOne));
		} else {
			orbitingVerse = 0;
		}

	}

	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		// angle in [-179,180]
		float signed_angle = angle * sign;
		return signed_angle;
	}


}
