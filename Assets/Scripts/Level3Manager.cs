using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour {

	public static Level3Manager instance;

	string prompt = "Level3 Prompt";

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
		GameManager.instance.setCurrentLevel(3);
		hudController.setTrial(3);
		hudController.reset();
		hudController.displayPrompt(prompt);
		Time.timeScale = 1;
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
