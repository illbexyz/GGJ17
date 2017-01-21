using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

	public GameObject player;
	public PlayerController pc;

	void OnCollisionEnter(Collision collision){
		Rigidbody body = player.GetComponent<Rigidbody> ();

		Vector3 normal = collision.contacts[0].normal;
		Vector3 vel = body.velocity;
		//Debug.Log ("Collision: " + vel);
		float angle = Vector3.Angle (vel, -normal);
		// measure angle

		Vector3 dir = Vector3.Reflect(body.transform.position, Vector3.right);
		//Debug.Log (dir);

		if (dir.x < -0.5) {
			Debug.Log(angle);
			pc.transform.Rotate (new Vector3(0f, 0f, 1f) * (180 - angle));
			body.velocity = Vector3.zero;
			body.isKinematic = true;
			pc.orbitingVerse = -1;
		} else if (dir.x > 0.5) {
			pc.transform.Rotate (new Vector3(0f, 0f, -1f) * (180 - angle));
			body.velocity = Vector3.zero;
			body.isKinematic = true;
			pc.orbitingVerse = 1;
		} else {
			GetComponent<SphereCollider> ().enabled = false;
			body.isKinematic = false;
			pc.orbitingVerse = 0;
		}
//		Debug.Log("angolo: " + angle);
//		if (angle < 180) {
//			//destra
//			pc.orbitingVerse = -1;
//		} else if (angle > 180) {
//			//sinistra
//			pc.orbitingVerse = 1;
//		} else {
//			//crash
//			Debug.Log("boom");
//		}
	}
}