using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    public GameObject player;
    private int hitPoints;
    private int maxHitPoints;
    Text mytext;

	// Use this for initialization
	void Start () {
        hitPoints = player.GetComponent<HealthPoints>().getHP();
        maxHitPoints = player.GetComponent<HealthPoints>().getMaxHP();
        mytext = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        hitPoints = player.GetComponent<HealthPoints>().getHP();
        mytext.text = "HP:        " + hitPoints + "/" + maxHitPoints;
	}
}
