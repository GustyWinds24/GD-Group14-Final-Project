using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RygelianCarrier : Enemy {

	public float randomDestinationRange = 15;
	public float stoppingDistance = 2;
	public float randomDirectionWaitTime = 10;
	public float targetingRange = 20;
	public float timeBetweenPukes = 5;

	public AudioClip pukeClip, dieClip;

	Vector3 target;
	bool targetStatus = false;
	bool foundRandPoint = false;
	bool targetAquired = false;
	float checkDistanceTimer = 0f;
	float randomDirectionTimer = 10f;
	float pukeTimer = 0f;
	ParticleSystem pukeParticles;
	GameObject childSpawnLocation;
	AudioSource audioSource;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		childSpawnLocation = transform.GetChild(2).gameObject;
		audioSource = GetComponent<AudioSource>();
		pukeParticles = GetComponentInChildren<ParticleSystem>();
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
			isDead = true;
			audioSource.clip = dieClip;
			audioSource.Play();
			animator.SetTrigger("cancelPuke");
			animator.SetTrigger("death");
			agent.enabled = false;
			Destroy(gameObject, secondsBeforeDestroy);
			return;
		}

		checkDistanceTimer += Time.deltaTime;
		randomDirectionTimer += Time.deltaTime;

		//If player within target radius
		if (targetStatus) {
			//If Close enough, stop setting destination to player, and just rotate to face the player
			if (Vector3.Distance(transform.position, player.transform.position) <= stoppingDistance)
					transform.rotation = Quaternion.Euler(0, Quaternion.FromToRotation(Vector3.forward, player.transform.position - transform.position).eulerAngles.y, 0);
			//Else, continue to move towards player
			else agent.SetDestination(player.transform.position);

			//Handle puke timer
			pukeTimer += Time.deltaTime;
			if (!isDead && pukeTimer >= timeBetweenPukes) {
				agent.isStopped = true;
				audioSource.clip = pukeClip;
				audioSource.Play();
				animator.SetTrigger("puke");
				StartCoroutine("puke");
				pukeTimer = 0f;
			}
		}
		//PLayer not within target radius && we haven't found a random point yet
		else if (randomDirectionTimer >= randomDirectionWaitTime && !foundRandPoint) {

			foundRandPoint = moveAgentToRandom();
			if (foundRandPoint) {
				randomDirectionTimer = 0f;
				foundRandPoint = false;
			}
		}

		//Scan for player
		if (checkDistanceTimer >= 1) {
			float distance = Vector3.Distance(transform.position, player.transform.position);
			if (2 <= distance && distance <= targetingRange) {
				if (!targetAquired) {
					//Audio
					targetAquired = true;
				}
				targetStatus = true;
			}
			else {
				if (targetAquired) targetAquired = false;
				targetStatus = false;
			}
			checkDistanceTimer = 0f;
		}

		if (Vector3.Distance(agent.velocity, Vector3.zero) > 0) animator.SetBool("moving", true);
		else animator.SetBool("moving", false);
	}

	bool moveAgentToRandom() {

		bool result = false;
		Vector3 randPoint = Vector3.zero;

		randPoint = Random.insideUnitCircle * randomDestinationRange;
		randPoint += agent.transform.position;

		NavMeshHit hit;
		if (NavMesh.SamplePosition(randPoint, out hit, 1f, NavMesh.AllAreas)) {

			agent.SetDestination(hit.position);
			result = true;
		}
		return result;
	}

	IEnumerator puke() {
		yield return new WaitForSeconds(.5f);
		pukeParticles.Play();
		yield return new WaitForSeconds(.5f);
		Instantiate(Resources.Load("Prefabs/RygelianChild") as GameObject, childSpawnLocation.transform.position, Quaternion.FromToRotation(transform.position, player.transform.position));
	}

	public void pukeFinished() {if (agent.enabled) agent.isStopped = false;}
	public void deathFinished() {sink = true;}
}
