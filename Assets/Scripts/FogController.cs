using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
	public GameObject controller;
	public GameObject me;
	public  Transform lastChild;
	public AudioSource sfx;

	public float fireRate, timer;

	void Start(){
		transform.position = new Vector3 (
			controller.transform.position.x,
			controller.transform.position.y,
			-3);
	}

	void Update(){
		timer = timer - Time.deltaTime;
		transform.position = new Vector3 (
			controller.transform.position.x,
			controller.transform.position.y,
			-3);
	}

	public void Light() {
		if (timer <= 0) {
			if (GameManager.instance.energy > 0) {
				sfx.Play ();
				GameManager.instance.DecrementEnergy ();
				StartCoroutine (LargeView ());
				timer = timer + fireRate;
			}
		}
	}
		
	IEnumerator LargeView(){
		while (lastChild.parent!=null) {
			lastChild.gameObject.SetActive(false);
			lastChild = lastChild.parent;
			yield return new WaitForSeconds (0.05f);
		}

		yield return new WaitForSeconds (0.2f);
		//lastChild.gameObject.SetActive(true);
		while (lastChild.childCount > 0 && lastChild.GetChild(0)!=null) {
			lastChild.gameObject.SetActive(true);
			lastChild = lastChild.GetChild(0);
			yield return new WaitForSeconds (0.05f);
		}
			
	}
		
}
