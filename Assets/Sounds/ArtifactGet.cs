using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactGet : MonoBehaviour {

    public AudioClip wonSound;
    private AudioSource cameraAudio;
    private AudioSource audio;
    private GameObject escapeTrigger;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        escapeTrigger = GameObject.FindGameObjectWithTag("Trigger2");
        cameraAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            escapeTrigger.GetComponent<EscapeTrigger>().artifactIsGot = true;
            cameraAudio.loop = false;
            cameraAudio.Stop();
            cameraAudio.clip = wonSound;
            cameraAudio.Play();
            Destroy(gameObject);
        }
    }
}
