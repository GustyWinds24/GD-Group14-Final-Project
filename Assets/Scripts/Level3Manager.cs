using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : MonoBehaviour {

	public static Level3Manager instance;

	string prompt = "Level3 Prompt";

	[SerializeField] GameObject artifactDoor;
	[SerializeField] AudioClip artifactDoorAudioClip;

	AudioSource soundEffects;
	HUDController hudController;

	[HideInInspector] public bool dragonDead;

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
		soundEffects = GetComponent<AudioSource>();
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

	public void gameOver() {

		Time.timeScale = 0;
		//Debug.Log(string.Format("{0} is setting timeScale to zero", gameObject.name));
		hudController.gameOver();
	}

	public bool tryToOpenArtifactDoor() {
		if (!dragonDead) {
			GameManager.instance.playLockedDoorSound();
			return false;
		}

		soundEffects.clip = artifactDoorAudioClip;
		soundEffects.Play();
		artifactDoor.GetComponent<Animator>().SetTrigger("open");
		return true;
	}
}
