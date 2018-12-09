using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHead : MonoBehaviour {

	ParticleSystem fire;

	private void Awake() {
		fire = GetComponent<ParticleSystem>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			other.gameObject.GetComponent<HealthPoints>().removeHealth(20);
			//Debug.Log("DragonHead hit player");
		}
	}

	public void startFire() {
		fire.Play();
	}
	
	public void stopFire() {
		fire.Stop();
	}
}
