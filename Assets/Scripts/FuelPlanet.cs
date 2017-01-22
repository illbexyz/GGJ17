using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPlanet : MonoBehaviour {

	void OnCollisionEnter(Collision c) {
		if (c.contacts [0].otherCollider.tag == "Player") {
			GameManager.instance.energy = GameManager.MAX_ENERGY;
		}
	}

}
