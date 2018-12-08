using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivated : MonoBehaviour {

    public GameObject activatedObject;
    public Vector3 direction;
    private bool switchActivated;

    // Use this for initialization
    void Start () {
        switchActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (switchActivated == true)
        {
            activatedObject.transform.position += direction;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            switchActivated = true;
            gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            switchActivated = true;
            gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }
    }
}
