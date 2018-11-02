using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // <-add this for UI stuff
using UnityEngine.SceneManagement;

public class TimeDisplay : MonoBehaviour
{
    private GameObject player;
    Text mytext;
    public float getTime;
    public string currentLevelName;
    public GameObject GameOverPanel;
    public GameObject pausePanel;
    private int timeFlag;
    private float privTime;

    // Use this for initialization
    void Start()
    {
        privTime = getTime;
        timeFlag = 0;
        player = GameObject.Find("Player");
        //lostPanel.SetActive(false);
        pausePanel.SetActive(false);
        mytext = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else if (privTime <= 0)
        {
            mytext.text = "Time: " + (int)privTime;
            Time.timeScale = 0f;
            GameOverPanel.SetActive(true);
        }
        else if(timeFlag == 1)
        {
            privTime -= Time.deltaTime;
            mytext.text = "Time: " + (int)privTime;
        }
    }

    public void activateTimeFlag()
    {
        timeFlag = 1;
    }

    // use to indicate reaching the end of a level as in getting to the stairs
    public void deactivateTimeFlag()
    {
        timeFlag = 0;
        privTime = getTime;
        player.GetComponent<HealthPoints>().myHealth = 100;
        Time.timeScale = 1f;
    }

    public void RestartButtonPressed()
    {
        timeFlag = 0;
        privTime = getTime;
        player.GetComponent<HealthPoints>().myHealth = 100;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevelName);
    }

    public void ContinueButtonPressed()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButtonPressed()
    {
        timeFlag = 0;
        privTime = getTime;
        player.GetComponent<HealthPoints>().myHealth = 100;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
