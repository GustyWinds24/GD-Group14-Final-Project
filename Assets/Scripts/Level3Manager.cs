using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3Manager : LevelManager {

    public GameObject winPanel;
    public GameObject gate1;
    public static Level3Manager instance;

    //Only grabbing this panel because it is scene specific
    public GameObject countDownTimerPanel;

    bool level3TimerOn = false;
    int seconds = 180;
    float level3Timer = 0f;

    Text countDownTimerText;

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
		myLevel = 3;
	}

	// Use this for initialization
	new void Start () {

		//Must come before updating level
		base.Start();

        winPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickMainMenu);
        winPanel.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickQuit);
        countDownTimerText = countDownTimerPanel.transform.GetChild(0).gameObject.GetComponent<Text>();

        GameManager.instance.setCurrentLevel(3);
		hudController.setTrial(3);
		hudController.displayPrompt(prompt);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime == 0) return;

        if (level3TimerOn)
        {
            countDownTimerPanel.SetActive(true);
            level3Timer += Time.deltaTime;
            if (level3Timer >= 1)
            {
                seconds--;
                level3Timer = 0f;
                countDownTimerText.text = string.Format("{0}", seconds);
            }
            if (seconds <= 0)
            {
                level3TimerOn = false;
                countDownTimerPanel.SetActive(false);
                GameManager.instance.gameOver();
            }
        }
    }

    public void startLevel3Timer()
    {
        countDownTimerPanel.SetActive(true);
        level3TimerOn = true;
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

		hudController.gameOver();
	}

    public void wonGame()
    {

        Time.timeScale = 0;
        //Debug.Log(string.Format("{0} is setting timeScale to zero", gameObject.name));
		GameManager.instance.doScores();
		hudController.setGameWonScore();
        winPanel.SetActive(true);
		GameManager.instance.resetScores();
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
