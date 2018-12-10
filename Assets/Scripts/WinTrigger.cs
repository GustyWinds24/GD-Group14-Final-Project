using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {

    public AudioSource winAudio;
    public bool artifactIsGot;
    private AudioSource cameraAudio;
    private AudioSource triggerAudio;

    // Use this for initialization
    void Start () {
        winAudio = GetComponent<AudioSource>();
        cameraAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        triggerAudio = GameObject.FindGameObjectWithTag("Box1").GetComponent<AudioSource>();
        artifactIsGot = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && artifactIsGot == true)
        {
            cameraAudio.loop = false;
            cameraAudio.Stop();
            triggerAudio.loop = false;
            triggerAudio.Stop();
            winAudio.Play();
            Level3Manager.instance.wonGame();
        }
    }
}
