using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FallOffPlatformController : MonoBehaviour {

    public float dropPoint;

    // Use this for initialization
    void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < dropPoint)
        {
            GameManager.instance.gameOver();
        }
	}
}
