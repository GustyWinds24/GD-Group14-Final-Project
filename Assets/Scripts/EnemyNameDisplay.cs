using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNameDisplay : MonoBehaviour {

    private string EnemyName;
    Text mytext;

	// Use this for initialization
	void Start () {
        mytext = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        mytext.text = "Enemy:  " + EnemyName;
    }

    public void setEnemyName(string name)
    {
        EnemyName = name;
    }
}
