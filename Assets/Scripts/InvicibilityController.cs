using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibilityController : MonoBehaviour {

    HealthPoints healthPoints;

	// Use this for initialization
	void Start () {
		healthPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPoints>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playInvincibleChipsSound();
            healthPoints.InvincibilityPowerUp();
            Destroy(gameObject);
        }
    }
}
