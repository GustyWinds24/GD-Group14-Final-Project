using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : Enemy {

	[SerializeField] float range;
	[SerializeField] float attackTime;

	[SerializeField] AudioClip[] audioClips;

	enum DragonAudio {Steps, Roar, Breathing, Attack1, Attack2};
	enum DragonState {Walking, Attacking, Idling};

	AudioSource stepsAudioSource, breathingAudioSource, attackAudioSource;
	DragonHead dragonHead;
	DragonState state = DragonState.Idling;
	GameObject head;
	ParticleSystem blood;

	bool walkingSet;
	bool inRange;
	bool stepsPlaying;
	float timer;

	private void Awake() {
		
		player = GameObject.FindGameObjectWithTag("Player");
		head = GameObject.FindGameObjectWithTag("DragonHead");
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		dragonHead = head.GetComponent<DragonHead>();
		blood = GameObject.FindGameObjectWithTag("DragonParticles").GetComponent<ParticleSystem>();

		var audio = GetComponents<AudioSource>();
		stepsAudioSource = audio[0];
		breathingAudioSource = audio[1];
		attackAudioSource = audio[2];
	}

	// Use this for initialization
	void Start () {
		
		/*
		Debug.Log(string.Format("player == {0}", player.name ?? "null"));
		Debug.Log(string.Format("head == {0}", head.name ?? "null"));
		Debug.Log(string.Format("agent == {0}", agent.name ?? "null"));
		Debug.Log(string.Format("animator == {0}", animator.name ?? "null"));
		Debug.Log(string.Format("dragonHead == {0}", dragonHead.name ?? "null"));
		Debug.Log(string.Format("blood == {0}", blood.name ?? "null"));
		*/

		agent.isStopped = true;
		stepsAudioSource.clip = audioClips[(int)DragonAudio.Steps];
		breathingAudioSource.clip = audioClips[(int)DragonAudio.Breathing];
		breathingAudioSource.loop = true;
		breathingAudioSource.Play();

		stepsAudioSource.loop = true;
		attackAudioSource.clip = audioClips[(int)DragonAudio.Attack1];
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) return;

		if (isDead) {
			if (sink) {
				transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			}
			return;
		}
		
		var distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = (distance <= range);

		if (state != DragonState.Attacking && inRange) {
			if (!walkingSet) setWalking();
			timer += Time.deltaTime;
			if (timer >= attackTime) {
				setAttacking();
				timer = 0;
				return;
			}
			agent.SetDestination(player.transform.position);
		}

		//Debug.Log(string.Format("attacking == {0} | inRange == {1} | agent.isStopped == {2}", attacking, inRange, agent.isStopped));
	}

	void setWalking() {
		stepsAudioSource.Play();
		breathingAudioSource.Play();
		agent.isStopped = false;
		state = DragonState.Walking;
		animator.SetTrigger("walk");
		walkingSet = true;
	}

	void setAttacking() {	
		walkingSet = false;
		breathingAudioSource.Stop();
		stepsAudioSource.Stop();
		agent.isStopped = true;
		state = DragonState.Attacking;
		animator.SetTrigger("breathFire");
	}

	public void startAttack() {
		attackAudioSource.Play();
	}

	public void stopAttack() {
		state = DragonState.Idling;
	}

	public void startFire() {
		dragonHead.startFire();
	}

	public void stopFire() {
		dragonHead.stopFire();
	}

	override public void takeDamage(int damage) {
		if (isDead) return;
		health -= damage;
		hitCount++;
		if (health <= 0) {
			stepsAudioSource.Stop();
			attackAudioSource.Stop();
			breathingAudioSource.Stop();
			breathingAudioSource.clip = audioClips[(int)DragonAudio.Roar];
			breathingAudioSource.loop = false;
			breathingAudioSource.Play();
			health = 0;
			agent.enabled = false;
			if (state == DragonState.Attacking) stopAttack();
			stopFire();
			animator.SetTrigger("die");
			isDead = true;
			Level3Manager.instance.dragonDead = true;
			int points = pointValue / hitCount;
			Debug.Log(string.Format("{0} was hit {1} times and is rewarding {2} points", enemyName, hitCount, points));
			GameManager.instance.collectPoints(points);
		}
		blood.Play();
	}

	public void deathFinished() {
		sink = true;
		GetComponent<BoxCollider>().enabled = false;
		Destroy(gameObject, secondsBeforeDestroy);
	}
}
