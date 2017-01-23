using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public GameObject controller;
	public GameObject fogController;
	public GameObject restartText;

	public int orbitingVerse;
	public bool started;

	private Vector3 prevVel;
	private Vector3 gravityCenter;

	private Collider lastOrbit;

	private bool isOrbitating = false;
	private int counter = 0;
	private float lastTimeCheck;

	public static int CLOCKWISE = -1;
	public static int CRASHING = 0;
	public static int ANTICLOCKWISE = 1;
	private const int deadAngle = 20;

	private void DetectSpaceBar(Rigidbody body) {
		if (Input.GetKeyDown (KeyCode.Space)) {
			body.isKinematic = false;
			orbitingVerse = 2;
			body.velocity = transform.right * 10;
			isOrbitating = false;
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
		Vector3 pos = transform.position;
		transform.position = pos;
		if (Input.GetKeyDown(KeyCode.P)) {
			fogController.GetComponent<FogController> ().Light ();
		}
		if (!started) {
			InitialMove (body);
		}
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
		Collider collider = c.contacts [0].otherCollider;
		if (collider == lastOrbit) {
			return;
		}
		if (collider.tag == "Orbit") {
			lastOrbit = collider;
			gravityCenter = collider.transform.position;
			Vector3 fwd = GetComponent<Rigidbody> ().transform.right;
//			Debug.Log ("fwd: " + fwd);
			Vector3 collisionPoint = c.contacts [0].point;
			Vector3 playerCenter = GetComponent<Rigidbody> ().position;

			Vector3 fromCollisionToCenter = gravityCenter - collisionPoint;
//			Vector3 tangent = Quaternion.Euler (0, 0, 90) * fromCollisionToCenter;

			Vector3 normal = c.contacts [0].normal;
			Vector3 vel = GetComponent<Rigidbody> ().velocity;
//			float angleFromOrbit = Vector3.Angle (tangent, fwd);

			Vector3 fromPlayerToCollisionPoint = collisionPoint - playerCenter;

			float signedAngle = SignedAngleBetween (fromPlayerToCollisionPoint, fwd, -fromCollisionToCenter);
//			Debug.Log ("signedAngle: " + signedAngle);
			isOrbitating = true;
			if (signedAngle > deadAngle) {
				transform.Rotate (0f, 0f, -(90 - signedAngle));
				orbitingVerse = ANTICLOCKWISE;
			} else if (signedAngle < -deadAngle) {
				orbitingVerse = CLOCKWISE;
				transform.Rotate (0f, 0f, (90 + signedAngle));
			} else {
				orbitingVerse = CRASHING;
				// GameManager.instance.RestartLevel ();
				if (collider.tag == "Orbit")
					collider.enabled = false;
			}
		} else if (collider.tag == "Boundaries") {
			lastTimeCheck = Time.time;
			StartCoroutine(askForRestart ());
		} else {
			lastTimeCheck = Time.time;
			StartCoroutine(askForRestart ());

		}

	}

	IEnumerator askForRestart() {
		while (Time.time < lastTimeCheck + 1) {
			yield return null;
		}
		if (GameManager.instance.energy <= 0) {
			restartText.GetComponent<Text> ().text = "GAME OVER!\nPress R to restart";
		} else {
			restartText.GetComponent<Text> ().text = "Press R to restart";
		}
		restartText.GetComponent<Text> ().enabled = true;
	}

	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n) {
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		// angle in [-179,180]
		float signed_angle = angle * sign;
		return signed_angle;
	}

	private void InitialMove(Rigidbody body){
		if(Input.GetKeyDown(KeyCode.A) && counter < 1){
			transform.Rotate (0, 0, 45);
			counter++;
		}
		if(Input.GetKeyDown(KeyCode.D) && counter > -1){
			transform.Rotate (0, 0, -45);
			counter--;
		}
		if(Input.GetKey(KeyCode.Space)){
			body.velocity = transform.right * 10;
			started = true;
			GameManager.instance.DecrementEnergy ();
		}
	}


}
