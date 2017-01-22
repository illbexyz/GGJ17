using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public GameObject player;

	void Update () {
		transform.Rotate (0, 0, 15 * Time.deltaTime);
	}

	void OnCollisionEnter(Collision c) {
		if (c.contacts [0].otherCollider.tag == "Player"
			&& player.GetComponent<PlayerController>().orbitingVerse != PlayerController.CRASHING) {
			c.contacts [0].otherCollider.enabled = false;
			GameManager.instance.NextLevel ();
		}
	}

}
