using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	float EASY = .5f, MEDIUM = 1, HARD = 2;

    public static GameManager instance;

	public AudioClip medkitAudio;
	public AudioClip DamageBottleSound;
    public AudioClip fastCanSound;
    public AudioClip invincibleChipsSound;
	public AudioClip scanning;
	[HideInInspector] public int points;
	[HideInInspector] public int highestScoreOnRecord;
	public float difficultyMultiplier = 1;

	[SerializeField] AudioClip lockedDoor;

    int currentLevel;

	AudioSource soundEffects;
	HUDController hud;

	public bool playerBeatLevel;
	[HideInInspector] public int levelStartingPoints;
	string highScore = "HighestScore";

    private void Awake()
    {

		if (PlayerPrefs.HasKey(highScore)) {
			highestScoreOnRecord = PlayerPrefs.GetInt(highScore);
			Debug.Log(string.Format("HighestScore on record is {0}", highestScoreOnRecord));
		}
		else {
			Debug.Log("No HighestScore on record");
		}

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

	public void collectPoints(int points) {
		this.points += points;
		hud.updateScore();
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

	//Only call this when player completes level
	public void nextLevel() {

		playerBeatLevel = true;
		GameManager.instance.saveLevelStartPoints();
		loadLevel(currentLevel + 1);
	}

	public void onClickRestart() {
		SceneManager.LoadScene("Level" + currentLevel);
	}

	public void onClickMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void onClickQuit() {
		Application.Quit();
	}

	public void gameOver() {

		Time.timeScale = 0;

		doScores();

		switch (currentLevel) {

			case 0:
				TutorialManager.instance.gameOver();
				break;

			case 1:
				Level1Manager.instance.gameOver();
				break;

			case 2:
				Level2Manager.instance.gameOver();
				break;

			case 3:
				Level3Manager.instance.gameOver();
				break;
		}
	}

	public void gameWon() {
		doScores();
	}

	public void doScores() {
		if (PlayerPrefs.HasKey(highScore)) {
			highestScoreOnRecord = PlayerPrefs.GetInt(highScore);
			if (points > highestScoreOnRecord) {
				Debug.Log(string.Format("New High Score, {0}", points));
				highestScoreOnRecord = points;
				PlayerPrefs.SetInt(highScore, points);
			}
			else {
				Debug.Log(string.Format("Didn't beat Highest score on record, {0}", highestScoreOnRecord));
			}
		}
		else {
			Debug.Log(string.Format("Setting HighestScore to {0}", points));
			PlayerPrefs.SetInt(highScore, points);
		}
	}

	public void setDifficulty(string s) {
		switch (s) {
			case "easy":
				difficultyMultiplier = EASY;
				break;

			case "medium":
				difficultyMultiplier = MEDIUM;
				break;

			case "hard":
				difficultyMultiplier = HARD;
				break;
		}
		Debug.Log(string.Format("Difficult was changed. Current difficultyMultiplier == {0:F2}", difficultyMultiplier));
	}

	public void playMedkitSound() {
		soundEffects.clip = medkitAudio;
		soundEffects.Play();
	}

	public void playDamageBottleSound() {
		soundEffects.clip = DamageBottleSound;
		soundEffects.Play();
	}

    public void playFastCanSound()
    {
        soundEffects.clip = fastCanSound;
        soundEffects.Play();
    }

    public void playInvincibleChipsSound()
    {
        soundEffects.clip = invincibleChipsSound;
        soundEffects.Play();
    }

    public void playScanningSound() {
		soundEffects.clip = scanning;
		soundEffects.Play();
	}

	public void playLockedDoorSound() {
		soundEffects.clip = lockedDoor;
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

	public void saveLevelStartPoints() {
		levelStartingPoints = points;
	}

	public void reloadLevelStartPoints() {
		points = levelStartingPoints;
	}

	public bool checkRestartingLevel(int level) {
		bool restart = false;
		if (level == currentLevel) {

			points = levelStartingPoints;
			restart = true;
		}
		return restart;
	}

	public void resetScores() {
		points = 0;
		levelStartingPoints = 0;
	}

	public void setHUDReference(HUDController hud) {this.hud = hud;}
}
