using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelController : MonoBehaviour {


	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {

			GameManager.instance.playScanningSound();
			Level1Manager.instance.collectIntel();
			Destroy(gameObject);
		}
	}
}
