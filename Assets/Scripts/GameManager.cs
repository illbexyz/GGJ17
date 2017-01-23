using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	private const int FUMETTO = 1;
	private const int PUZZLE = 2;

	public AudioSource fumettoTrack;
	public AudioSource puzzleTrack;

	public static GameManager instance = null;
	public static int MAX_ENERGY = 30;

	private Queue<string> levels;
	private string currentLevel;

	public int energy = MAX_ENERGY;

	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);	
		DontDestroyOnLoad(gameObject);

		resetLevels ();

		fumettoTrack.Play ();

	}

	void resetLevels () {
		currentLevel = "Fumetto1";
		levels = new Queue<string> (new [] {
			"Puzzle1", 
			"Puzzle2", "Fumetto2", "Puzzle3",
			"Fumetto3", "Puzzle4", "Fumetto4",
		});
	}

	void ChangeMusic( string next) {
		if (!currentLevel.StartsWith (next.Substring(0,3))) {
			if (currentLevel.StartsWith ("Puz")) {
//				track = fumettoTrack;
				puzzleTrack.Stop();
				fumettoTrack.Play ();
			} else {
//				track = puzzleTrack;
				fumettoTrack.Stop();
				puzzleTrack.Play();
			}
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			RestartLevel ();
		}
	}

	public void NextLevel() {
//		level++;
//		SceneManager.LoadScene ("Level" + level);
		Debug.Log(levels.Count);
		if (levels.Count == 0) {
			Application.Quit ();
			return;
		}
		string nextLevel = levels.Dequeue();
		ChangeMusic (nextLevel);
		currentLevel = nextLevel;
		SceneManager.LoadScene(currentLevel);
	}
	
	public void RestartLevel() {
		if (energy > 0) {
			SceneManager.LoadScene (currentLevel);
		} else {
			GameOver ();
		}
	}

	public void GameOver() {
		ChangeMusic ("Puzzle1");
		energy = 30;
		UpdateEnergyText ();
		resetLevels ();
		SceneManager.LoadScene (levels.Dequeue());
	}

	private void UpdateEnergyText() {
		Text text = transform.GetChild (0).GetChild (0).GetComponent<Text>();
		text.text = "Energy: " + energy;
	}

	public void DecrementEnergy() {
		energy--;
		UpdateEnergyText ();
	}

	public void ResetEnergy() {
		energy = MAX_ENERGY;
		UpdateEnergyText ();
	}

//	IEnumerator printEnergy() {
//		while (true) {
//			Debug.Log ("Energy: " + energy);
//			yield return new WaitForSeconds (1f);
//		}
//	}

}
