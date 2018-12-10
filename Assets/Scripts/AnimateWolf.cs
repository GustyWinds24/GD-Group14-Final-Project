using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateWolf : Enemy {

    // current position of wolves
    float currentPositionX;
    float currentPositionZ;
    float startPositionX;
    float startPositionZ;

    // for the player when he steps into the navmesh
    ulong count = 0;
    Transform playerTransform;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        currentPositionX = transform.position.x;
        currentPositionZ = transform.position.z;
        startPositionX = currentPositionX;
        startPositionZ = currentPositionZ;
	}
	
	// Update is called once per frame
	void Update () {
        currentPositionX = transform.position.x;
        currentPositionZ = transform.position.z;
		if (currentPositionX != startPositionX && currentPositionZ != startPositionZ)
        {
            animator.SetBool("run", true);
        }
        if (isDead != true)
        {
            if (health <= 0)
            {
                isDead = true;
                animator.SetTrigger("dead");
                agent.enabled = false;
                Destroy(gameObject, 5f);
            }
        }
        if (count % 100 == 0)
        {
            if (agent.enabled == true) agent.SetDestination(playerTransform.position);
            this.
            count = 0;
        }
        count++;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            Debug.Log("Enemy trigger with player");
            animator.SetTrigger("attack");
            other.gameObject.GetComponent<HealthPoints>().removeHealth(strength);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {

            Debug.Log("Enemy collision with player");
            animator.SetTrigger("attack");
            collision.gameObject.GetComponent<HealthPoints>().removeHealth(strength);
        }
    }
}
