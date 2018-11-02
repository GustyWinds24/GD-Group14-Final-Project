using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMove : MonoBehaviour {

    public GameObject MainPanel;
    public GameObject LoadPanel;
    public GameObject OptionsPanel;
    private string TutLevel;
    private string LoadGame;

    // Use this for initialization
    void Start () {
        TutLevel = "Level0";
        LoadGame = "LoadScreen";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButton()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene(TutLevel);
    }

    public void LoadButton()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene(LoadGame);
    }

    public void OptionsButton()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene("Options");
    }

    public void CreditsButton()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
