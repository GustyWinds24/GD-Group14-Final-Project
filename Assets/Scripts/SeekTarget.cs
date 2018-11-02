using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekTarget : MonoBehaviour
{
    Animator anim;
    public Transform target;
    public int removeHealthAmount;
    NavMeshAgent agent;
    int currentPosition;
    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        anim.SetBool("run", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("attack");
            collision.gameObject.GetComponent<HealthPoints>().removeHealth(removeHealthAmount);
        }
    }
}