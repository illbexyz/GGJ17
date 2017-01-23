using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
	public GameObject controller;
	public GameObject me;
	public  Transform lastChild;

<<<<<<< HEAD
=======
	public float fireRate, timer;

>>>>>>> ebf3789a73650f01f1fa933348aee4ab07aa4b34
	void Start(){
		transform.position = new Vector3 (
			controller.transform.position.x,
			controller.transform.position.y,
			-3);
	}

	void Update(){
<<<<<<< HEAD
		if (Input.GetKey (KeyCode.Space)) {
			StartCoroutine (LargeView ());
		}

=======
		timer = timer - Time.deltaTime;
>>>>>>> ebf3789a73650f01f1fa933348aee4ab07aa4b34
		transform.position = new Vector3 (
			controller.transform.position.x,
			controller.transform.position.y,
			-3);
	}
<<<<<<< HEAD
=======

	public void Light() {
		if (timer <= 0) {
			GameManager.instance.energy--;
			StartCoroutine (LargeView ());
			timer = timer + fireRate;
		}
	}
>>>>>>> ebf3789a73650f01f1fa933348aee4ab07aa4b34
		
	IEnumerator LargeView(){
		while (lastChild.parent!=null) {
			lastChild.gameObject.SetActive(false);
			lastChild = lastChild.parent;
<<<<<<< HEAD
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (0.5f);
		//lastChild.gameObject.SetActive(true);

		while (lastChild.GetChild(0)!=null) {
			lastChild.gameObject.SetActive(true);
			lastChild = lastChild.GetChild(0);
			yield return new WaitForSeconds (0.1f);
=======
			yield return new WaitForSeconds (0.05f);
		}

		yield return new WaitForSeconds (0.2f);
		//lastChild.gameObject.SetActive(true);
		while (lastChild.childCount > 0 && lastChild.GetChild(0)!=null) {
			lastChild.gameObject.SetActive(true);
			lastChild = lastChild.GetChild(0);
			yield return new WaitForSeconds (0.05f);
>>>>>>> ebf3789a73650f01f1fa933348aee4ab07aa4b34
		}
			
	}
		
}
