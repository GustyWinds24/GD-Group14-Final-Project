using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour {

	[SerializeField] int fireDamage, particlesBeforeDamage;
	[SerializeField] int deathY;
	int particlesHit;

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
		if (Time.timeScale == 0) return;
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
		if (myHealth == 0) GameManager.instance.gameOver();
		if (transform.position.y <= deathY) GameManager.instance.gameOver();
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
		var damage = health * GameManager.instance.difficultyMultiplier;
		Debug.Log(string.Format("DifficultyMulti == {0}", GameManager.instance.difficultyMultiplier));
		Debug.Log(string.Format("Removing {0} health from player. DifficultyMultiplier is {1:F2}", (int)damage, GameManager.instance.difficultyMultiplier));
		health = (int)damage;
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

	private void OnParticleCollision(GameObject other) {
		
		particlesHit++;
		if (particlesHit >= particlesBeforeDamage) {
			removeHealth(fireDamage);
			particlesHit = 0;
		}
		//Debug.Log("Player is hit with fire");
	}

	public GameObject getTarget() {return target;}

    public void InvincibilityPowerUp()
    {
        oldHealth = myHealth;
        invincibilityMode = true;
    }
}
