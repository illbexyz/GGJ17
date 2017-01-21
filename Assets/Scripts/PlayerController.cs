using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject controller;

	public int orbitingVerse;

	private Vector3 prevVel;
	private Vector3 gravityCenter;

	private bool isOrbitating = false;
	private const int CLOCKWISE = -1;
	private const int CRASHING = 0;
	private const int ANTICLOCKWISE = 1;


	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		Vector3 push = new Vector3 (10.0f, 0.0f, 0.0f);
		//body.AddForce (push);
		body.velocity = transform.right * 10;
	}

	private void DetectSpaceBar(Rigidbody body) {
		if (Input.GetKey (KeyCode.Space)) {
			body.isKinematic = false;
			orbitingVerse = 2;
			body.velocity = transform.right * 10;
		}
	}

	private void SetOrbitatingBehaviour(Rigidbody body, Vector3 verse) {
		body.velocity = Vector3.zero;
		body.isKinematic = true;
		transform.RotateAround (gravityCenter, verse, 100 * Time.deltaTime);
		DetectSpaceBar (body);
	}

	void Update(){
		Rigidbody body = GetComponent<Rigidbody> ();

		Vector3 verse = new Vector3 (0.0f, 0.0f, 1.0f * orbitingVerse);
		if (isOrbitating) {
			if (orbitingVerse == CLOCKWISE) {
				// Orario
				SetOrbitatingBehaviour(body, verse);
			} else if (orbitingVerse == ANTICLOCKWISE) {
				// Antiorario
				SetOrbitatingBehaviour(body, verse);
			} else if (orbitingVerse == CRASHING) {
				// Usiamo la velocita' salvata prima della collisione
				body.velocity = prevVel;
			}
		} else {
			// Is going in straight line
			prevVel = body.velocity;
		}

	}

	void OnCollisionEnter(Collision c){
		gravityCenter = c.contacts [0].otherCollider.transform.position;
		Vector3 fwd = GetComponent<Rigidbody> ().transform.right;
		Vector3 collisionPoint = c.contacts [0].point;
		Vector3 playerCenter = GetComponent<Rigidbody> ().position;

		Vector3 normal = c.contacts[0].normal;
		Vector3 vel = GetComponent<Rigidbody> ().velocity;
		float angleFromOrbit = Vector3.Angle (vel, -normal);

		Vector3 fromPlayerToCollisionPoint = collisionPoint - playerCenter;
					
		float signedAngle = SignedAngleBetween (fromPlayerToCollisionPoint, fwd, transform.position.normalized);
		isOrbitating = true;
		if (signedAngle > 0) {
			transform.Rotate (new Vector3(0f, 0f, 1f) * (180+angleFromOrbit));
			orbitingVerse = ANTICLOCKWISE;
		} else if (signedAngle < 0) {
			orbitingVerse = CLOCKWISE;
			transform.Rotate (new Vector3(0f, 0f, -1f) * (180+angleFromOrbit));
		} else {
			orbitingVerse = CRASHING;
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
