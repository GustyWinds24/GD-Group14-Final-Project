using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAlarmSoundLoop : MonoBehaviour {

    private AudioSource audio;
    private bool loopIsOn;
    private float loopTimer = 0f;

	// Use this for initialization
	void Start () {
        loopIsOn = false;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(loopIsOn == true)
        {
            loopTimer += Time.deltaTime;
            if(loopTimer >= 10)
            {
                audio.loop = false;
                loopIsOn = false;
            }
        }
	}

    public void PlayAlarmSound()
    {
        loopIsOn = true;
        audio.loop = true;
        audio.Play();
    }
}
