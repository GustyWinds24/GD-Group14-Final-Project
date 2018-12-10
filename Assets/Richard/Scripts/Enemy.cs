using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public string enemyName;
	public int health, maxHealth;
	public int strength;
	public float secondsBeforeDestroy = 7f;
	public int pointValue;

	protected Animator animator;
	protected NavMeshAgent agent;
	protected GameObject player;
	protected GameObject playerTarget;
	protected bool isDead = false;
	protected bool sink = false;
	protected int hitCount;
	protected float sinkSpeed = 2.5f;

	public virtual void takeDamage(int damage) {
		if (isDead) return;
		health -= damage;
		hitCount++;
		if (health <= 0) {
			health = 0;
			var colliders = GetComponents<Collider>();
			foreach (Collider c in colliders) {
				Debug.Log(string.Format("Disabling {0} (Collider)", c.name));
				c.enabled = false;
			}
			int points = pointValue / hitCount;
			Debug.Log(string.Format("{0} was hit {1} times and is rewarding {2} points", enemyName, hitCount, points));
			GameManager.instance.collectPoints(points);
		}
	}
}
