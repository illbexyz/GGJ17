using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public static int MAX_ENERGY = 30;

	private Queue<string> levels;
	public int level = 1;
	public int energy = MAX_ENERGY;

	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);	
		DontDestroyOnLoad(gameObject);

		levels = new Queue<string> ( new [] {
			"Menu", "Fumetto1", "Puzzle1", "Battle1", 
			"Puzzle2", "Battle2", "Fumetto2", "Puzzle3",
			"Battle3", "Fumetto3", "Puzzle4", "Fumetto4"
		});

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			RestartLevel ();
		}
	}

	public void NextLevel() {
		level++;
		SceneManager.LoadScene ("Level" + level);
	}
	
	public void RestartLevel() {
		SceneManager.LoadScene ("Level" + level);
	}

	public void GameOver() {
		SceneManager.LoadScene ("Level1");
		energy = 30;
	}

//	IEnumerator printEnergy() {
//		while (true) {
//			Debug.Log ("Energy: " + energy);
//			yield return new WaitForSeconds (1f);
//		}
//	}

}
