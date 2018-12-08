using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2ExitDoorListener : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Level2Manager.instance.levelComplete();
        }
    }
}
