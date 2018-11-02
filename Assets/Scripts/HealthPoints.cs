using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour {

    public int myHealth = 100;
    public int maxHealth = 100;
	
	// Update is called once per frame
	void Update () {
		if(myHealth == 0)
        {
            GameManager.instance.gameOver();
        }
	}

    public void addHealth(int health)
    {
        if((myHealth + health) >= maxHealth)
        {
            myHealth = maxHealth;
        }
        else
        {
            myHealth += health;
        }
    }

    public void removeHealth(int health)
    {
        myHealth -= health;
        if(myHealth < 0)
        {
            myHealth = 0;
        }
    }

    public int getHP()
    {
        return myHealth;
    }

    public int getMaxHP()
    {
        return maxHealth;
    }

	public void collectMedkit(int health) {
		myHealth += health;
		if (myHealth >= 100) myHealth = 100;
	}
}
