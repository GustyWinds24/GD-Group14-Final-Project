using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Manager : LevelManager {

    public GameObject gate1;
    public static Level3Manager instance;

    bool hasKey1 = false, hasKey2 = false, hasKey3 = false;
    public AudioClip pickUpKeySound, openDoorSound, lockedDoorSound;

    string prompt = "You finally made it to the top. Wait, what's that sound? It looks like " +
                    "they had some guardian guarding the artifact. We just got intel telling us " +
                    "of its name. It some ancient revived beast by the name of \"Rounos\". This is " +
                    "your final trial. Take out the guardian \"Rounos\" and retrieve the artifact " +
                    "for disposal. Be careful out there and use everything that you have learned from " +
                    " previous trials. Command out!";

	[SerializeField] GameObject artifactDoor;
	[SerializeField] AudioClip artifactDoorAudioClip;

	AudioSource soundEffects;

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

		soundEffects = GetComponent<AudioSource>();
	}

	// Use this for initialization
	new void Start () {
		base.Start();
		GameManager.instance.setCurrentLevel(3);
		hudController.setTrial(3);
		hudController.displayPrompt(prompt);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime == 0) return;
	}

    public bool getHasKey1() { return hasKey1; }
    public bool getHasKey2() { return hasKey2; }
    public bool getHasKey3() { return hasKey3; }
    void setHasKey1() { hasKey1 = true; }
    void setHasKey2() { hasKey2 = true; }
    void setHasKey3() { hasKey3 = true; }

    public void collectKey(string key)
    {
        switch (key)
        {

            case "Key1":
                setHasKey1();
                break;
            case "Key2":
                setHasKey2();
                break;
            case "Key3":
                setHasKey3();
                break;
        }
    }

    public bool tryToOpenGate(string gate)
    {
        bool hasKey = false;
        switch (gate)
        {
            case "Gate1":
                if (hasKey1 == true && hasKey2 == true && hasKey3 == true)
                {
                    //Open Gate 1
                    gate1.GetComponent<Animator>().SetTrigger("open");
                    hasKey = true;
                }
                break;
        }

        if (hasKey) playOpenDoorSound();
        else playLockedDoorSound();

        return hasKey;
    }

    public bool hasKeyForGate(string gate)
    {
        bool result = false;
        switch (gate)
        {
            case "Gate1":
                result = getHasKey1();
                break;
            case "Gate2":
                result = getHasKey2();
                break;
            case "Gate3":
                result = getHasKey3();
                break;
        }
        return result;
    }

    public void playOpenDoorSound()
    {
        soundEffects.clip = openDoorSound;
        soundEffects.Play();
    }
    public void playLockedDoorSound()
    {
        soundEffects.clip = lockedDoorSound;
        soundEffects.Play();
    }
    public void playPickUpKeySound()
    {
        soundEffects.clip = pickUpKeySound;
        soundEffects.Play();
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
