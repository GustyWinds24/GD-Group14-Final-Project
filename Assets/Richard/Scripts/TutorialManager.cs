using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : LevelManager {
	
	public static TutorialManager instance;
	
	//Only grabbing this panel because it is scene specific
	public GameObject countDownTimerPanel;

	public float dropPoint;

	bool tutorialTimerOn = false;
	int seconds = 60;
	float tutorialTimer = 0f;

	GameObject player;
	GameObject hud;
	Text countDownTimerText;

	string[] prompt =	{
					"Welcome to the tutorial of Trials of the Floating Tower. Here you will learn some of the basics to be able to beat a level. " +
					"First off, use the WASD keys in order to move around. Click the left mouse button to fire your rifle. Click the right mouse button to " +
					"throw a grenade. Use the keys 1 and 2 to cycle between rifle and pistol. Use the Escape key to pause the game. " +
					"Rotate with the mouse. Click Ok to begin. W - Move up-right  A - Move up-left  S - Move down-left  D - Move down-right " +
					"1 - Switch to pistol  2 - Switch to rifle  Esc - pause the game",

					"You can jump by pressing the spacebar. Stand still and press it to jump in place. You can jump in any direction as long " +
					"as you are pressing one of the direction keys first. Try jumping over the gap near the bottom right of your screen.",

					"You can also crouch by holding shift. You can use it to shoot enemies lower than you. You also have the ability to push " +
					"boxes by just running into them. Try it out with the crates north of here. One more thing, you will be encountering your first " +
					"box puzzle. If you mess up, you can continue on or fall off the floating island to restart the tutorial.",

					"Within the each level there will be many obstacles that will require a switch or key to open them. This obstacle requires a " +
					"switch to open it. To use a switch, just touch it by running into it to change its color. This will lower the obstacle revealing " +
					"the path behind it. There should be one in this area. Look for it.",

					"Look out! Alien mutts! You can't shoot them standing up! Remember what to do with enemies smaller than you. Though " +
					"you can still throw grenades, they can still avoid them. If they touch you, you lose health! Run if they get too near! " +
					"Also, you can see their name and current health status on the left of your screen. This can help you keep track " +
					"of who you're facing.",

					"Items are scattered around through each level. Pick one up and you can be healed, granted a temporary speed boost, receive " +
					"more ammo and much more! Down here is a health pack. Later on you will find bottles that grants you x3 damage. " +
					"Just understand that power ups only last 10 seconds so make them count.",

					"You are about to face enemies at your height. These enemies are different than the ones you faced off earlier since these " +
					"enemies can fire shots at you. Remember to shoot and throw grenades to hurt them. To dodge their attacks just simply move " +
					"to the side of their bullet trajectory.",

					"Timed runs are common on some levels. Remember that your current goal in your mission is to get into the tower before the " +
					"security system awakens and shuts the door on you. You forgot? Well no time to lose! You have 1 minute to reach the " +
					"door! Hurry!",

					"Congratulations on beating the tutorial level. Hopefully with all you learned throughout will help you on your journey " +
					"to reaching the artifact at the top of the tower. Now go into the doorway to begin your first trial. Just don't stand around " +
					"after hitting \"ok\" or else you will lose to the timer effectively closing the door and reactivating the security system."
	};

	private void Awake() {
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

		player = GameObject.FindGameObjectWithTag("Player");
		hud = GameObject.FindGameObjectWithTag("HUD");
		myLevel = 0;
	}

	// Use this for initialization
	new void Start () {
		base.Start();
		GameManager.instance.setCurrentLevel(0);
		hudController = hud.GetComponent<HUDController>();
		countDownTimerText = countDownTimerPanel.transform.GetChild(0).gameObject.GetComponent<Text>();

		//string hudName = hudController == null ? "null" : hudController.name;
		//Debug.Log(string.Format("hudController name == {0}", hudName));
		//Debug.Log("TutorialManager is calling Start(). Setting timeScale = 1");
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.timeScale == 0) return;

		if (tutorialTimerOn) {
			countDownTimerPanel.SetActive(true);
			tutorialTimer += Time.deltaTime;
			if (tutorialTimer >= 1) {
				seconds--;
				tutorialTimer = 0f;
				countDownTimerText.text = string.Format("{0}", seconds);
			}
			if (seconds <= 0) {
				tutorialTimerOn = false;
				countDownTimerPanel.SetActive(false);
				GameManager.instance.gameOver();
			}
		}

		if (player.transform.position.y < dropPoint) {
			GameManager.instance.gameOver();
		}
	}

	public void displayPrompt(int num) {

		hudController.displayPrompt(prompt[num]);
		if (num == 7) startTutorialTimer();
		Time.timeScale = 0;
		//Debug.Log(string.Format("{0} is setting timeScale to zero", gameObject.name));
	}

	public void disablePrompt() {
		hudController.disablePrompt();
		Time.timeScale = 1;
		//Debug.Log(string.Format("{0} is setting timeScale to 1", gameObject.name));
	}

	public void startTutorialTimer() {

		countDownTimerPanel.SetActive(true);
		tutorialTimerOn = true;
	} 

	public void gameOver() {
		countDownTimerPanel.SetActive(false);
		hudController.gameOver();
	}

	public void pauseGame() {hudController.displayPauseMenu();}
	public void unpauseGame() {hudController.disablePauseMenu();}
}
