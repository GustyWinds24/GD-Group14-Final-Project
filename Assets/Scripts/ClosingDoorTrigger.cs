using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingDoorTrigger : MonoBehaviour {

    public GameObject triggerSoundObject;
    public GameObject closingDoors;
    public GameObject trigger1;
    public GameObject trigger2;

    private playAlarmSoundLoop alarmPlay;
    private Animator anim;

	// Use this for initialization
	void Start () {
        alarmPlay = triggerSoundObject.GetComponent<playAlarmSoundLoop>();
        anim = closingDoors.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            alarmPlay.PlayAlarmSound();
            anim.SetTrigger("close");
            Destroy(trigger1);
            Destroy(trigger2);
            Destroy(gameObject);
        }

    }
}
