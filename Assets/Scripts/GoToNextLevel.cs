using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextLevel : MonoBehaviour {

    public string nextLevelName;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
			GameManager.instance.setCurrentLevel(1);
        }
    }
}
