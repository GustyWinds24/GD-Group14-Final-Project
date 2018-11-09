using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RygelianChild : Enemy {

	public AudioClip spawnClip, dieClip;

	bool started = false;
	float waitToRun = 0;
	HealthPoints playerHealth;
	Animator deathAnimator;
	AudioSource audioSource;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		playerHealth = player.GetComponent<HealthPoints>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.deltaTime == 0) return;

		if (isDead) {
			if (sink) transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			return;
		}

		if (health <= 0) {
			//Audio
			audioSource.clip = dieClip;
			audioSource.Play();
			isDead = true;
			agent.enabled = false;
			animator.SetBool("moving", false);
			sink = true;
			Destroy(gameObject, secondsBeforeDestroy);
			return;
		}

		if (!started) {
			waitToRun += Time.deltaTime;
			if (waitToRun >= .5f) {
				started = true;
				audioSource.clip = spawnClip;
				audioSource.Play();
			}
			else return;
		}

		if (started) agent.SetDestination(player.transform.position);

		if (Vector3.Distance(agent.velocity, Vector3.zero) > 0) animator.SetBool("moving", true);
		else animator.SetBool("moving", false);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			playerHealth.removeHealth(strength);
		}
	}
}
