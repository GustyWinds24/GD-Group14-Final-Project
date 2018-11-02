using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour {

    Text mytext;
    public int levelNumber = 1;

	// Use this for initialization
	void Start () {
        mytext = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        mytext.text = "Trial: " + levelNumber;
    }
}
