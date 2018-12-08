using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzleSwitchActivated : MonoBehaviour {

    public GameObject activatedObject;
    public Vector3 direction;
    public GameObject trigger1;
    public GameObject trigger2;
    private bool trigger1Bool;
    private bool trigger2Bool;
    private bool switchActivated;

    // Use this for initialization
    void Start()
    {
        //trigger1 = GameObject.FindGameObjectWithTag("Trigger1");
        //trigger2 = GameObject.FindGameObjectWithTag("Trigger2");
        switchActivated = false;
        trigger1Bool = false;
        trigger2Bool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger1Bool == true && trigger2Bool == true)
        {
            switchActivated = true;
        }
        if (switchActivated == true && gameObject.CompareTag("Trigger1") == true)
        {
            Debug.Log("1st door down");
            activatedObject.transform.position += direction;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Box2"))
        {
            trigger1.GetComponent<BoxPuzzleSwitchActivated>().trigger1Bool = true;
        } else if (collider.CompareTag("Box1"))
        {
            trigger1.GetComponent<BoxPuzzleSwitchActivated>().trigger2Bool = true;
        }
    }
}
