using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

    string gate;
    BoxCollider myCollider;

	// Use this for initialization
	void Start () {
        gate = this.tag;
        myCollider = gameObject.GetComponent<BoxCollider>();
	}


    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.tag;
        bool opened = false;

        switch (otherTag)
        {

            case "Player":
                opened = Level3Manager.instance.tryToOpenArtifactDoor();
                break;
        }
        if (opened) BoxCollider.Destroy(myCollider);
    }
}
