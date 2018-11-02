using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBottleController : MonoBehaviour {

	RifleController rifleController;
	public int multiplier;

	private void Awake() {
		rifleController = GameObject.FindGameObjectWithTag("Player").GetComponent<RifleController>();
	}

	private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("Player")) {
			GameManager.instance.playDamageBottleSound();
			rifleController.damageBottle(multiplier);
			Destroy(gameObject);
		}
	}
}
