using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1ExitDoorListener : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Level1Manager.instance.levelComplete();
		}
	}
}
