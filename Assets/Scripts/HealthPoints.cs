using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour {

    public int myHealth = 100;
    public int maxHealth = 100;
    public int oldHealth;
    private float invincibilityTimer = 0f;
    private bool invincibilityMode;

	GameObject target;

	private void Awake() {
        invincibilityMode = false;
		target = transform.GetChild(5).gameObject;
	}

	// Update is called once per frame
	void Update () {
        if(invincibilityMode == true)
        {
            myHealth = maxHealth;
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= 10)
            {
                myHealth = oldHealth;
                invincibilityMode = false;
                invincibilityTimer = 0f;
            }
        }
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
		if (myHealth > maxHealth) myHealth = maxHealth;
	}

	public GameObject getTarget() {return target;}

    public void InvincibilityPowerUp()
    {
        oldHealth = myHealth;
        invincibilityMode = true;
    }
}
