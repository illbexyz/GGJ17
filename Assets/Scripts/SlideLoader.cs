using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideLoader : MonoBehaviour {


	public RawImage frame;
	private Queue<string> imagesName;

	public string second, third;


	void Start(){

		imagesName = new Queue<string> ( new [] {
			second,
			third
		});
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (imagesName.Count> 0) {
				Texture newTexture = Resources.Load <Texture> (imagesName.Dequeue ());

				frame.texture = newTexture;
			} else {
				GameManager.instance.NextLevel ();
			}

		}
	}
}
