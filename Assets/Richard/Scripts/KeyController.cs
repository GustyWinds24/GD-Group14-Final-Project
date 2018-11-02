using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {

    string myTag;

    private void Start()
    {
        myTag = this.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        switch (tag)
        {
            case "Player":
                Level1Manager.instance.playPickUpKeySound();
                Level1Manager.instance.collectKey(myTag);
				switch(myTag) {
					case "Key1":
						Level1Manager.instance.openShortcut1();
						break;
					case "Key2":
						Level1Manager.instance.openShortcut2();
						break;
				}
                Destroy(gameObject);
                break;
        }
    }
}
