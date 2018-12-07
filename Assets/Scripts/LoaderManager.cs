using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour {

    public GameObject MainPanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Level0Loader()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene("Level0");
    }

    public void Level1Loader()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene("Level1");
    }

    public void Level2Loader()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene("Level2");
    }

    public void Level3Loader()
    {
        MainPanel.SetActive(false);
        SceneManager.LoadScene("Level3");
    }
}
