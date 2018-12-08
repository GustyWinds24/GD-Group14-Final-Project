using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivated : MonoBehaviour {

    private AudioSource audio;

    public GameObject activatedObject;
    public Vector3 direction;
    private bool switchActivated;
    private Vector3 level2ElevatedPlatform;

    // Use this for initialization
    void Start () {
        audio = gameObject.GetComponent<AudioSource>();
        switchActivated = false;
        level2ElevatedPlatform = new Vector3(-4, 0.4f, 8.529f);
    }
	
	// Update is called once per frame
	void Update () {
        if (switchActivated == true && activatedObject.CompareTag("Trigger1") == true)
        {
            if (activatedObject.transform.position != level2ElevatedPlatform) {
                activatedObject.transform.position = Vector3.MoveTowards(activatedObject.transform.position, level2ElevatedPlatform, 0.1f);
            }
        }
        else if (switchActivated == true)
        {
            audio.Play();
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

}
