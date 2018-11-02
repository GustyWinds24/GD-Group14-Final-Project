using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedboxController : MonoBehaviour {

	HealthPoints playerHealth;
	public int health;

	private void Awake() {
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPoints>();
	}

	private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("Player")) {
			GameManager.instance.playMedkitSound();
			playerHealth.collectMedkit(health);
			Destroy(gameObject);
		}
	}
}
