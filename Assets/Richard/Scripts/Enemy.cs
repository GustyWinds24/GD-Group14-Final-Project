using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public string enemyName;
	public int health, maxHealth;
	public int strength;

	Animator animator;
	NavMeshAgent agent;
	bool isDead = false;

	private void Awake() {
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead) return;
        if (health <= 0) {
			isDead = true;
			animator.SetTrigger("dead");
			agent.enabled = false;
			Destroy(gameObject, 5f);
		}
        animator.SetBool("run", true);
    }

	public void takeDamage(int damage) {
		if (isDead) return;
		health -= damage;
		if (health <= 0) health = 0;
	}

	private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("Player")) {

			Debug.Log("Enemy trigger with player");
            animator.SetTrigger("attack");
            other.gameObject.GetComponent<HealthPoints>().removeHealth(strength);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		
		if (collision.collider.CompareTag("Player")) {

			Debug.Log("Enemy collision with player");
            animator.SetTrigger("attack");
            collision.gameObject.GetComponent<HealthPoints>().removeHealth(strength);
		}
	}
}
