using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour {

	public static Level2Manager instance;

	string prompt = "Level2 Prompt";

	HUDController hudController;

	private void Awake() {
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
		hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
	}

	// Use this for initialization
	void Start () {
		GameManager.instance.setCurrentLevel(2);
		hudController.setTrial(2);
		hudController.reset();
		hudController.displayPrompt(prompt);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime == 0) return;
	}

	public void onClickPrompt() {
		hudController.disablePrompt();
	}

	public void pauseGame() {
		hudController.displayPauseMenu();
	}

	public void unpauseGame() {
		hudController.disablePauseMenu();
	}
}
