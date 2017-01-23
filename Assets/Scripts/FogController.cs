using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
	public GameObject controller;
	public GameObject me;
	public  Transform lastChild;

	void Start(){
		transform.position = new Vector3 (
			controller.transform.position.x,
			controller.transform.position.y,
			-3);
	}

	void Update(){
		if (Input.GetKey (KeyCode.Space)) {
			StartCoroutine (LargeView ());
		}

		transform.position = new Vector3 (
			controller.transform.position.x,
			controller.transform.position.y,
			-3);
	}
		
	IEnumerator LargeView(){
		while (lastChild.parent!=null) {
			lastChild.gameObject.SetActive(false);
			lastChild = lastChild.parent;
			yield return new WaitForSeconds (0.1f);
		}

		yield return new WaitForSeconds (0.5f);
		//lastChild.gameObject.SetActive(true);

		while (lastChild.GetChild(0)!=null) {
			lastChild.gameObject.SetActive(true);
			lastChild = lastChild.GetChild(0);
			yield return new WaitForSeconds (0.1f);
		}
			
	}
		
}
