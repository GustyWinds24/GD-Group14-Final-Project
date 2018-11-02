using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthPoints : MonoBehaviour {

    Animator anim;
    public int myhealth;
    public int maxHealth;
    public string enemyName;
    public GameObject enemyHealthDisplay;
    public GameObject enemyNameDisplay;
    public GameObject enemyStatusPanel;
    private int getTime;
    private int isDeadFlag;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        isDeadFlag = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (myhealth <= 0)
        {
            enemyNameDisplay.GetComponent<EnemyNameDisplay>().setEnemyName("");
            if(isDeadFlag == 0)
            {
                dead();
            }
        }
    }

    public void removeHealth(int health)
    {
        myhealth -= health;
        if (myhealth < 0)
        {
            myhealth = 0;
        }
    }

    public void showEnemyHealth()
    {
        enemyNameDisplay.GetComponent<EnemyNameDisplay>().setEnemyName(enemyName);
        enemyHealthDisplay.GetComponent<EnemyHealthDisplay>().setEnemy(gameObject);
        enemyHealthDisplay.GetComponent<EnemyHealthDisplay>().setHitPoints();
        getTime = 3;
        enemyStatusPanel.SetActive(true);
        new WaitForSeconds(2.5f);
        //enemyStatusPanel.SetActive(false);
    }

    public int getHP()
    {
        return myhealth;
    }

    public int getMaxHP()
    {
        return maxHealth;
    }

    public void dead()
    {
        isDeadFlag = 1;
        anim.SetTrigger("dead");
        Destroy(gameObject, 5f);
    }
}
