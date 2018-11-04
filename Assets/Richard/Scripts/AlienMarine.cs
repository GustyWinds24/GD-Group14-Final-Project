using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienMarine : Enemy {

	GameObject player;

	bool aiming = false;
	bool foundPlayer = false;
	bool foundRandPoint = false;
	bool effectsOn = false;
	float checkDistanceTimer = 0f;
	float randomDirectionTimer = 10f;
	float shootTimer = 0f;
	public float timeBetweenShots = 2;
	LineRenderer gunLine;
	Transform gun;
	Vector3 miss0, miss1, miss2;
	Ray ray;
	RaycastHit hit;
	AudioSource gunAudio;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		gun = transform.GetChild(0).gameObject.transform;
		gunLine = GetComponent<LineRenderer>();
		gunAudio = GetComponent<AudioSource>();
	}

	private void Start() {
		miss0 = Vector3.zero;
		miss1 = new Vector3 (.05f, 0, 1);
		miss2 = new Vector3 (-.05f, 0, 1);
		ray = new Ray();
		hit = new RaycastHit();
	}

	// Update is called once per frame
	void Update () {
		
		if (Time.deltaTime == 0) return;

		if (health <= 0) {
			isDead = true;
			animator.SetTrigger("dead");
			agent.enabled = false;
			Destroy(gameObject, 4.5f);
			enabled = false;
			return;
		}

		checkDistanceTimer += Time.deltaTime;
		randomDirectionTimer += Time.deltaTime;

		if (foundPlayer) {
			if (Vector3.Distance(transform.position, player.transform.position) <= 2)
					transform.rotation = Quaternion.Euler(0, Quaternion.FromToRotation(Vector3.forward, player.transform.position - transform.position).eulerAngles.y, 0);
			else agent.SetDestination(player.transform.position);
			shootTimer += Time.deltaTime;
			if (shootTimer >= timeBetweenShots) {
				shoot();
				shootTimer = 0f;
			}
			if (effectsOn && shootTimer >= (timeBetweenShots * .01f)) disableEffects();
		}
		else if (randomDirectionTimer >= 10 && !foundRandPoint) {

			foundRandPoint = moveAgentToRandom();
			if (foundRandPoint) {
				randomDirectionTimer = 0f;
				foundRandPoint = false;
			}
		}

		if (checkDistanceTimer >= 1) {
			float distance = Vector3.Distance(transform.position, player.transform.position);
			if (2 <= distance && distance <= 15) {
				foundPlayer = true;
				if (!aiming) {
					animator.SetTrigger("aim");
					aiming = true;
				}
			}
			else {
				foundPlayer = false;
				animator.SetTrigger("backToIdle");
				aiming = false;
			}
			checkDistanceTimer = 0f;
		}
	}

	bool moveAgentToRandom() {

		bool result = false;
		Vector3 randPoint = Vector3.zero;

		randPoint = Random.insideUnitCircle * 30;
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

		gunAudio.Play();
		int rand = Random.Range(0, 3);
		Vector3 direction = gun.InverseTransformDirection(Vector3.forward);
		switch (rand) {
			case 0:
				direction += miss0;
				break;
			case 1:
				direction += miss1;
				break;
			case 2:
				direction += miss2;
				break;
		}

		ray.origin = gun.position;
		ray.direction = gun.forward;

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
}
