using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public GameObject hudPanel;
	public GameObject pausePanel;
	public GameObject promptPanel;
	public GameObject gameOverPanel;
	public GameObject enemyStatusPanel;

	[SerializeField] Text liveScoreValue;
	[SerializeField] Text[] endGameScoreDisplay;

	int GAME_OVER = 0, GAME_WON = 1;

	GameObject player;
	RifleController rifleController;
	HealthPoints playerHealth;
	Text timeText;
	Text trialText;
	Text currentWeaponText;
	Text playerHP;
	Text weaponAmmo;
	Text enemyName;
	Text enemyHP;
	Text promptText;

	int seconds = 0;
	int minutes = 0;
	float timer = 0f;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");

		timeText = hudPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
		trialText = hudPanel.transform.GetChild(1).gameObject.GetComponent<Text>();
		currentWeaponText = hudPanel.transform.GetChild(2).gameObject.GetComponent<Text>();
		playerHP = hudPanel.transform.GetChild(3).gameObject.GetComponent<Text>();
		weaponAmmo = hudPanel.transform.GetChild(4).gameObject.GetComponent<Text>();

		enemyName = enemyStatusPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
		enemyHP = enemyStatusPanel.transform.GetChild(1).gameObject.GetComponent<Text>();

		playerHealth = player.GetComponent<HealthPoints>();
		rifleController = player.GetComponent<RifleController>();
		promptText = promptPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		currentWeaponText.text = "Weapon: " + rifleController.getWeaponName();
		pausePanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickContinue);
		pausePanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickMainMenu);
		pausePanel.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickQuit);

		gameOverPanel.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickRestart);
		gameOverPanel.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickMainMenu);
		gameOverPanel.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(GameManager.instance.onClickQuit);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.deltaTime == 0) return;

		timer += Time.deltaTime;
		if (timer >= 1) {
			timer = 0f;
			seconds++;
			if (seconds >= 60) {
				minutes++;
				seconds = 0;
			}
		}

		updateTime();
		updateHP();
		updateAmmo();
	}

	void updateTime() {
		string timeString = string.Format("Time:  {0}' {1:D2}\"", minutes, seconds);
		timeText.text = timeString;
	}

	void updateTrial(int trial) {
		trialText.text = "Trial:  " + trial;
	}

	public void updateWeaponName(string name) {
		currentWeaponText.text = "Weapon:  " + name;
	}

	void updateHP() {
		playerHP.text = "HP: " + playerHealth.myHealth;
	}

	void updateAmmo() {
		weaponAmmo.text = "Ammo: " + rifleController.getAmmo();
	}

	public void setDisplayEnemy (Enemy enemy) {
		enemyName.text = "Enemy:  " + enemy.enemyName;
		enemyHP.text = string.Format("HP:       {0} / {1}", enemy.health, enemy.maxHealth);
		enemyStatusPanel.GetComponent<Animator>().SetTrigger("displayEnemy");
	}

	public void setTrial(int trial) {
		trialText.text = string.Format("Trial: {0}", trial);
	}

	public void gameOver() {
		endGameScoreDisplay[GAME_OVER].text = string.Format("Score:  {0}\nHigh Score:  {1}", GameManager.instance.points, GameManager.instance.highestScoreOnRecord);
		gameOverPanel.SetActive(true);
	}

	public void displayPrompt(string s) {

		promptText.text = s;
		promptPanel.SetActive(true);
	}

	public void disablePrompt() {
		promptPanel.SetActive(false);
	}

	public void reset() {
		pausePanel.SetActive(false);
		gameOverPanel.SetActive(false);
		promptPanel.SetActive(false);
	}

	public void setGameWonScore() {
		endGameScoreDisplay[GAME_WON].text = string.Format("Score:  {0}\nHigh Score:  {1}", GameManager.instance.points, GameManager.instance.highestScoreOnRecord);
	}

	public void updateScore() {liveScoreValue.text = GameManager.instance.points.ToString();}

	public void displayPauseMenu() {pausePanel.SetActive(true);}
	public void disablePauseMenu() {pausePanel.SetActive(false);}
}
