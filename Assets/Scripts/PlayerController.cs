using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	public bool isOrbiting;
	void Start () {
		Rigidbody body = GetComponent<Rigidbody> ();
		Vector3 push = new Vector3 (0.0f, 5.0f, 0.0f);
		body.AddForce (push * speed);
	}

	void Update(){
		if (isOrbiting) {
			Vector3 verse = new Vector3 (0.0f, 0.0f, 1.0f);
			transform.RotateAround(Vector3.zero, verse , 100 * Time.deltaTime);
		}

	}
}
