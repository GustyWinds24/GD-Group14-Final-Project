using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeTrigger : MonoBehaviour {

    public AudioClip escapeSound;
    public GameObject escapeEnemies;
    private AudioSource cameraAudio;
    private AudioSource triggerAudio;
    private AudioSource audio;
    public GameObject alertAudio;
    public bool artifactIsGot;
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;

    // Use this for initialization
    void Start () {
        key1.SetActive(false);
        key2.SetActive(false);
        key3.SetActive(false);
        artifactIsGot = false;
        audio = GetComponent<AudioSource>();
        cameraAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        triggerAudio = GameObject.FindGameObjectWithTag("Box1").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && artifactIsGot == true)
        {
            Level3Manager.instance.startLevel3Timer();
            escapeEnemies.SetActive(true);
            key1.SetActive(true);
            key2.SetActive(true);
            key3.SetActive(true);
            cameraAudio.Stop();
            cameraAudio.clip = escapeSound;
            cameraAudio.loop = true;
            cameraAudio.Play();
            triggerAudio.Play();
            alertAudio.GetComponent<AlertAudioPlay>().PlayAlertAudio();
            Destroy(gameObject);
        }
    }
}
