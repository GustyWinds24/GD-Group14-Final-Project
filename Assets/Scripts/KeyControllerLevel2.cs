using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControllerLevel2 : MonoBehaviour
{

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
                Level2Manager.instance.playPickUpKeySound();
                Level2Manager.instance.collectKey(myTag);
                switch (myTag)
                {
                    case "Key1":
                        Level2Manager.instance.tryToOpenGate("Gate1");
                        break;
                    case "Key2":
                        Level2Manager.instance.tryToOpenGate("Gate2");
                        break;
                }
                Destroy(gameObject);
                break;
        }
    }
}
