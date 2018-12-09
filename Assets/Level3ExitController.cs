using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3ExitController : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {if (other.CompareTag("Player")) Level3Manager.instance.tryToOpenArtifactDoor();}
}
