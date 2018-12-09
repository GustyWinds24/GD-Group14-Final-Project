using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertAudioPlay : MonoBehaviour {

    public AudioClip endOfTransmission;
    private AudioSource audio;
    private bool isLooping;
    private float alertTimer = 0f;

	// Use this for initialization
	void Start () {
        isLooping = false;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isLooping == true)
        {
            alertTimer += Time.deltaTime;
            if(alertTimer >= 45)
            {
                audio.loop = false;
                isLooping = false;
                audio.Stop();
                audio.clip = endOfTransmission;
                audio.Play();
            }
        }
	}

    public void PlayAlertAudio()
    {
        isLooping = true;
        audio.Play();
    }
}
