using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour {

    ulong count = 0;
    GameObject player;
    Transform playerTransform;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (count % 100 == 0)
        {
            agent.SetDestination(playerTransform.position);
            this.
            count = 0;
        }
        count++;
    }
}
