using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public string enemyName;
	public int health, maxHealth;
	public int strength;

	protected Animator animator;
	protected NavMeshAgent agent;
	protected bool isDead = false;

	public void takeDamage(int damage) {
		if (isDead) return;
		health -= damage;
		if (health <= 0) health = 0;
	}
}
