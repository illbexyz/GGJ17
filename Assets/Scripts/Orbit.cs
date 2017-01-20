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
		float angle = Vector3.Angle (vel, -normal);
		// measure angle
		if (angle < 90) {
			//sinistra
			//player.GetComponent<PlayerController>().isOrbiting = true;
			pc.isOrbiting = true;
			Debug.Log("sinistra");
		} else if (angle > 90) {
			//destra
			Debug.Log("destra");
			body.transform.Rotate(0,-20*Time.deltaTime,0);
		} else {
			//crash
			Debug.Log("boom");
		}
	}
}