using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	protected int myLevel;
	protected HUDController hudController;

	protected void Start() {
		
		bool checkRestart = GameManager.instance.checkRestartingLevel(myLevel);

		Debug.Log(string.Format("Level{0}Manager is checking restart", myLevel));

		if (checkRestart) {
			Debug.Log("CheckRestart is true");
			GameManager.instance.reloadLevelStartPoints();
		}
		else if (GameManager.instance.playerBeatLevel) {
			Debug.Log("playerBeatLevel is true");
			GameManager.instance.playerBeatLevel = false;
		}
		else {
			Debug.Log("Random load");
			GameManager.instance.points = 0;
		}

		getHUDReference();
		GameManager.instance.setHUDReference(hudController);
		hudController.reset();
		updateScore();
	}

	public void getHUDReference() {
		hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
	}

	public void updateScore() {
		hudController.updateScore();
	}
}
