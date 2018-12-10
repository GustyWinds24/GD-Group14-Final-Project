using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControllerLevel3 : MonoBehaviour {

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
                Level3Manager.instance.playPickUpKeySound();
                Level3Manager.instance.collectKey(myTag);
                switch (myTag)
                {
                    case "Key1":
                        Level3Manager.instance.tryToOpenGate("Gate1");
                        break;
                    case "Key2":
                        Level3Manager.instance.tryToOpenGate("Gate1");
                        break;
                    case "Key3":
                        Level3Manager.instance.tryToOpenGate("Gate1");
                        break;
                }
                Destroy(gameObject);
                break;
        }
    }
}
