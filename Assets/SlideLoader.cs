using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideLoader : MonoBehaviour {


	public Image frame;
	private Queue<string> imagesName;


	void Start(){
		Sprite newSprite =  Resources.Load <Sprite>("Pagina1/foglio1");

		imagesName = new Queue<string> ( new [] {
			"Pagina1/foglio1",
			"Pagina1/foglio2",
			"Pagina1/foglio3",
			"Pagina1/foglio4",
			"Pagina1/foglio5",
			"Pagina1/foglio6"
		});
	}

//	void Update () {
//		if (Input.GetKeyDown (KeyCode.RightArrow)) {
//			Sprite newSprite =  Resources.Load <Sprite>(imagesName.Enqueue);
//
//			if (newSprite) {
//				frame.sprite = newSprite;
//			} else {
//				//cambio di scena - fumetto finito
//			}
//
//		}
//	}
}
