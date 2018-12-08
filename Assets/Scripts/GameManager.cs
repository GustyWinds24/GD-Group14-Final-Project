using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

	public AudioClip medkitAudio;
	public AudioClip DamageBottleSound;
	public AudioClip scanning;

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

	private void Start() {
		//Test to see if this is called on load
		//Debug.Log("GameManager is calling Start()");
		Time.timeScale = 1;
	}

	private void Update() {
		
		if (Time.deltaTime == 0) return;
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			
			displayPauseMenu();
			//Debug.Log(string.Format("{0} is setting timeScale to zero", gameObject.name));
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
		//Debug.Log(string.Format("{0} is setting timeScale to 1", gameObject.name));
	}

	public void onClickRestart() {
		SceneManager.LoadScene("Level" + currentLevel);
		//Debug.Log(string.Format("{0} is setting timeScale to 1", gameObject.name));
		Time.timeScale = 1;
	}

	public void onClickMainMenu() {
		SceneManager.LoadScene("MainMenu");
		//Debug.Log(string.Format("{0} is setting timeScale to 1", gameObject.name));
		Time.timeScale = 1;
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

	public void playScanningSound() {
		soundEffects.clip = scanning;
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

			case 2:
				Level2Manager.instance.pauseGame();
				break;

			case 3:
				Level3Manager.instance.pauseGame();
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

			case 2:
				Level2Manager.instance.unpauseGame();
				break;

			case 3:
				Level3Manager.instance.unpauseGame();
				break;
		}
	}
}
