using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CiccioBot : Enemy {
	
	bool targetStatus = false;
	bool foundRandPoint = false;
	bool effectsOn = false;
	bool targetAquired = false;
	float checkDistanceTimer = 0f;
	float randomDirectionTimer = 10f;
	float shootTimer = 0f;
	public float timeBetweenShots = 5;
	public float stoppingDistance = 2;
	public float randomDestinationRange = 15;
	public float randomDirectionWaitTime = 10;
	public float targetingRange = 20;
	LineRenderer gunLine;
	Transform gun;
	Ray ray;
	RaycastHit hit;
	AudioSource soundEffects;
	public AudioClip ciccioTalkAudio, ciccioShootAudio, ciccioDieAudio;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		gun = transform.GetChild(0).gameObject.transform;
		gunLine = GetComponent<LineRenderer>();
		soundEffects = GetComponent<AudioSource>();
		playerTarget = player.gameObject.GetComponent<HealthPoints>().getTarget();
	}

	private void Start() {
		ray = new Ray();
		hit = new RaycastHit();
	}

	// Update is called once per frame
	void Update () {
		
		if (Time.deltaTime == 0) return;

		if (isDead) {
			if (sink) transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			return;
		}

		if (health <= 0) {
			playCiccioDieAudio();
			isDead = true;
			animator.SetTrigger("CancelAttack");
			animator.SetTrigger("Dead");
			disableEffects();
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

			//Handle shoot timer
			shootTimer += Time.deltaTime;
			if (shootTimer >= timeBetweenShots) {
				animator.SetTrigger("Attack");
				shootTimer = 0f;
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
					playCiccioTalkAudio();
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

		if (Vector3.Distance(agent.velocity, Vector3.zero) > 0) animator.SetBool("Move", true);
		else animator.SetBool("Move", false);

		if (effectsOn && shootTimer >= (timeBetweenShots * .01f)) disableEffects();
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

	private void OnTriggerEnter(Collider other) {
		
		if (other.CompareTag("Player")) {

			Debug.Log("Enemy trigger with player");
			other.gameObject.GetComponent<HealthPoints>().removeHealth(strength);
		}
	}

	void shoot() {

		playCiccioShootAudio();

		ray.origin = gun.position;
		ray.direction = playerTarget.transform.position - gun.position;

		if (Physics.Raycast(ray, out hit, 100)) {
			if (hit.collider.CompareTag("Player")) {
				hit.collider.gameObject.GetComponent<HealthPoints>().removeHealth(strength);
			}
		}

		gunLine.SetPosition(0, gun.position);
		gunLine.SetPosition(1, hit.point);
		gunLine.enabled = true;
		effectsOn = true;
	}

	void disableEffects() {
		gunLine.enabled = false;
		effectsOn = false;
	}

	void playCiccioTalkAudio() {
		soundEffects.clip = ciccioTalkAudio;
		soundEffects.Play();
	}

	void playCiccioShootAudio() {
		soundEffects.clip = ciccioShootAudio;
		soundEffects.Play();
	}

	void playCiccioDieAudio() {
		soundEffects.clip = ciccioDieAudio;
		soundEffects.Play();
	}

	public void deathFinished() {sink = true;}
}
