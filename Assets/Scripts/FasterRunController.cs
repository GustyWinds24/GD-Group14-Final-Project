using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterRunController : MonoBehaviour {

    PlayerController controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playFastCanSound();
            controller.changeAnimationSpeed();
            Destroy(gameObject);
        }
    }
}
