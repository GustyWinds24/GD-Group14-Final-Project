using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	protected HUDController hudController;

	protected void Start() {
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
