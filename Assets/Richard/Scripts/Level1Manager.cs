using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour {

	public GameObject gate1, gate2, gate3, gate4;
	public GameObject shortcut1, shortcut2;
	public GameObject hud;

    public AudioClip pickUpKeySound, openDoorSound, lockedDoorSound;

	public AudioSource level1Music;

    public static Level1Manager instance;

    bool hasKey1 = false, hasKey2 = false, hasKey3 = false; 
	//bool hasBigKey = false;

	AudioSource soundEffects;
	HUDController hudController;

	string prompt = "This is your first trial. Don't take it as lightly as you did outside. Explore around and find the keys to open the door to the next level/trial. " +
					"Be careful not to get hurt. I hope you're trigger happy.";

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

		soundEffects = GetComponent<AudioSource>();
		hudController = hud.gameObject.GetComponent<HUDController>();
    }

	private void Start() {
		Time.timeScale = 0;
		level1Music.Play();
		GameManager.instance.setCurrentLevel(1);
		hudController.setTrial(1);
		hudController.reset();
		hudController.displayPrompt(prompt);
	}

	private void Update()
    {
		if (Time.deltaTime == 0) return;

    }

    public bool getHasKey1 () { return hasKey1; }
    public bool getHasKey2 () { return hasKey2; }
    public bool getHasKey3 () { return hasKey3; }
    void setHasKey1 () { hasKey1 = true; }
    void setHasKey2 () { hasKey2 = true; }
    void setHasKey3 () { hasKey3 = true; }

    public void collectKey (string key)
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
                if (hasKey1)
                {
                    //Open Gate 1
					gate1.GetComponent<Animator>().SetTrigger("open");
                    hasKey = true;
                }
                else
                {
                    hasKey = false;
                }
                break;
            case "Gate2":
                if (hasKey2)
                {
                    //Open Gate 2
					gate2.GetComponent<Animator>().SetTrigger("open");
                    hasKey = true;
                }
                else
                {
                    hasKey = false;
                }
                break;
            case "Gate3":
                if (hasKey3)
                {
                    //Open Gate 3
					gate3.GetComponent<Animator>().SetTrigger("open");
                    hasKey = true;
                }
                else
                {
                    hasKey = false;
                }
                break;
            case "ExitDoor":
				//Open Exit Door
				gate4.GetComponent<Animator>().SetTrigger("open");
                hasKey = true;
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

    public void playOpenDoorSound() {
		soundEffects.clip = openDoorSound;
		soundEffects.Play();
	}
    public void playLockedDoorSound() {
		soundEffects.clip = lockedDoorSound;
		soundEffects.Play();
	}
    public void playPickUpKeySound() {
		soundEffects.clip = pickUpKeySound;
		soundEffects.Play();
	}
	
	public void levelComplete() {
		GameManager.instance.goHome();
	}

	public void onClickPrompt () {
		hudController.disablePrompt();
		Time.timeScale = 1;
	}

	public void gameOver() {
		Time.timeScale = 0;
		hudController.gameOver();
	}

	public void pauseGame() {hudController.displayPauseMenu();}
	public void unpauseGame() {hudController.disablePauseMenu();}
	public void openShortcut1() {shortcut1.GetComponent<Animator>().SetTrigger("open");}
	public void openShortcut2() {shortcut2.GetComponent<Animator>().SetTrigger("open");}
}
