using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour {

    GameObject player;
    GameObject hud;

    public float dropPoint;
    public GameObject gate1, gate2;
    public GameObject shortcut1, shortcut2;

    public Vector3 moveGateDown;

    public AudioClip pickUpKeySound, openDoorSound, lockedDoorSound;

    public static Level2Manager instance;

    bool hasKey1 = false, hasKey2 = false;
    bool door1Move = false, door2Move = false;


    string prompt = "Welcome to the second trial. Smaller but tougher with all those enemies around. " +
                    "Remember to push boxes whenever you feel like you're stuck! Same as before " +
                    "gather intel, kill some enemies and find the way up to the next trial.";

    AudioSource soundEffects;
    HUDController hudController;

	private void Awake() {

        moveGateDown = new Vector3(0, 0.2f, 0);
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        soundEffects = GetComponent<AudioSource>();
        hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

	// Use this for initialization
	void Start () {

        GameManager.instance.setCurrentLevel(2);
		hudController.setTrial(2);
		hudController.reset();
		hudController.displayPrompt(prompt);
		Time.timeScale = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0) return;
        if (door1Move == true)
        {
            Debug.Log("Door going down");
            gate1.transform.position = gate1.transform.position - moveGateDown;
        }
        if (door2Move == true)
        {
            gate2.transform.position = gate2.transform.position - moveGateDown;
        }
        if (player.transform.position.y < dropPoint)
        {
            gameOver();
        }
    }

    public bool getHasKey1() { return hasKey1; }
    public bool getHasKey2() { return hasKey2; }
    void setHasKey1() { hasKey1 = true; }
    void setHasKey2() { hasKey2 = true; }

    public void collectKey(string key)
    {
        switch (key)
        {

            case "Key1":
                Debug.Log("Key1 collected");
                setHasKey1();
                break;
            case "Key2":
                setHasKey2();
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
                    door1Move = true;
                    hasKey = true;
                }
                break;
            case "Gate2":
                if (hasKey2)
                {
                    //Open Gate 2
                    door2Move = true;
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

    public void levelComplete()
    {
        GameManager.instance.loadLevel((GameManager.instance.getCurrentLevel() + 1));
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

    public void gameOver()
    {
        hudController.gameOver();
        //Debug.Log(string.Format("{0} is setting timeScale to zero", gameObject.name));
        Time.timeScale = 0;
    }

    //public void openShortcut1() { shortcut1.GetComponent<Animator>().SetTrigger("open"); }
    //public void openShortcut2() { shortcut2.GetComponent<Animator>().SetTrigger("open"); }
}
