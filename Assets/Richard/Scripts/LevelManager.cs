using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	protected int myLevel;
	protected HUDController hudController;

	protected void Start() {
		
		bool checkRestart = GameManager.instance.checkRestartingLevel(myLevel);

		if (checkRestart) {
			GameManager.instance.reloadLevelStartPoints();
		}
		if (GameManager.instance.playerBeatLevel) {
			GameManager.instance.playerBeatLevel = false;
		}
		else {
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
