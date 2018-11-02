using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{

    private GameObject Enemy;
    private int hitPoints;
    private int maxHitPoints;
    //public GameObject EnemyStatusPanel;
    Text mytext;

    // Use this for initialization
    void Start()
    {
        //EnemyStatusPanel.SetActive(false);
        mytext = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints > 0)
        {
            mytext.text = "HP:        " + hitPoints + "/" + maxHitPoints;
        }
        else
        {
            mytext.text = "HP:        ";
        }
    }

    public void setHitPoints()
    {
        hitPoints = Enemy.GetComponent<EnemyHealthPoints>().getHP();
        maxHitPoints = Enemy.GetComponent<EnemyHealthPoints>().getMaxHP();
    }

    public void setEnemy(GameObject enemy)
    {
        Enemy = enemy;
    }
}