using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

	public AudioClip medkitAudio;
	public AudioClip DamageBottleSound;

    int currentLevel;

	AudioSource soundEffects;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
		soundEffects = GetComponent<AudioSource>();
    }

	private void Update() {
		
		if (Time.deltaTime == 0) return;
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			
			displayPauseMenu();
			Time.timeScale = 0;
		}
	}

	public int getCurrentLevel() { return currentLevel; }
    public void setCurrentLevel (int level) { currentLevel = level; }

    public void loadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

	public void onClickContinue() {

		disablePauseMenu();
		Time.timeScale = 1;
	}

	public void onClickRestart() {
		Time.timeScale = 1;
		SceneManager.LoadScene("Level" + currentLevel);
	}

	public void onClickMainMenu() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}

	public void onClickQuit() {
		Application.Quit();
	}

	public void gameOver() {

		switch (currentLevel) {

			case 0:
				TutorialManager.instance.gameOver();
				break;

			case 1:
				Level1Manager.instance.gameOver();
				break;
		}
	}

	public void playMedkitSound() {
		soundEffects.clip = medkitAudio;
		soundEffects.Play();
	}

	public void playDamageBottleSound() {
		soundEffects.clip = DamageBottleSound;
		soundEffects.Play();
	}

	public void goHome() {
		SceneManager.LoadScene("MainMenu");
	}

	public void displayPauseMenu() {

		switch (currentLevel) {

			case 0:
				TutorialManager.instance.pauseGame();
				break;

			case 1:
				Level1Manager.instance.pauseGame();
				break;
		}
	}

	public void disablePauseMenu() {

		switch (currentLevel) {

			case 0:
				TutorialManager.instance.unpauseGame();
				break;

			case 1:
				Level1Manager.instance.unpauseGame();
				break;
		}
	}
}
