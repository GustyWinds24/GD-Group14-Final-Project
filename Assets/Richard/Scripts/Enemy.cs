using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public string enemyName;
	public int health, maxHealth;
	public int strength;
	public float secondsBeforeDestroy = 7f;

	protected Animator animator;
	protected NavMeshAgent agent;
	protected GameObject player;
	protected GameObject playerTarget;
	protected bool isDead = false;
	protected bool sink = false;
	protected float sinkSpeed = 2.5f;

	public void takeDamage(int damage) {
		if (isDead) return;
		health -= damage;
		if (health <= 0) health = 0;
	}
}
