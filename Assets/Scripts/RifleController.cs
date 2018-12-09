using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon {
	public string name;
	public int damage;
	public int ammo;
	public Weapon (string name, int damage, int ammo) {
		this.name = name;
		this.damage = damage;
		this.ammo = ammo;
	}
}

public class RifleController : MonoBehaviour
{
	public AudioSource gunAudio;
	public AudioClip rifleShot;

	bool effectsOn = false;
	bool usingDamageBottle = false;
	int shootableMask;
	int oldDamage;
	float timer = 0.2f;
	float timeBetweenRifleBullets = 0.35f;
	float damageBottleTimer = 0f;

	GameObject rifleObject;
	GameObject rifleTip;
	LineRenderer gunLine;
	GameObject leftHand, rightHand;
	HUDController hudController;

	Weapon rifle;
	Weapon currentWeapon;

	private void Awake() {

        rifleObject = Instantiate(Resources.Load("Prefabs/Gun") as GameObject, Vector3.zero, Quaternion.identity);

        leftHand = transform.GetChild(0).gameObject;
        rightHand = transform.GetChild(1).gameObject;
        rifleObject.transform.SetParent(rightHand.transform, false);
		hudController = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDController>();
		rifle = new Weapon("Series4 Deatomizer", 10, 1000);
		currentWeapon = rifle;
	}

	private void Start() {
		rifleTip = rifleObject.transform.GetChild(1).gameObject;
		gunLine = rifleTip.GetComponentInChildren<LineRenderer>();
		gunAudio.clip = rifleShot;
	}

	void Update() {
		if (Time.deltaTime == 0) return;

		timer += Time.deltaTime;
		if (Input.GetButton("Fire1") && (timer >= timeBetweenRifleBullets)) shoot();
		if (effectsOn && timer >= (timeBetweenRifleBullets * .05f)) disableEffects();

		if (usingDamageBottle) {
			damageBottleTimer += Time.deltaTime;
			if (damageBottleTimer >= 10) {
				usingDamageBottle = false;
				currentWeapon.damage = oldDamage;
				damageBottleTimer = 0f;
			}
		}
	}

	void shoot() {
		gunAudio.Play();

		//gunLine.enabled = true;

		Ray ray = new Ray(rifleTip.transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();

		gunLine.SetPosition(0, rifleTip.transform.position);

        if (Physics.Raycast(ray, out hit, 200f)) {
            if (hit.collider.gameObject.CompareTag("Enemy")) {
				Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
				enemy.takeDamage(currentWeapon.damage);
				hudController.setDisplayEnemy(enemy);
				hudController.showEnemy();
            }
			gunLine.SetPosition(1, hit.point);
			gunLine.enabled = true;
			effectsOn = true;
			//Debug.Log(string.Format("Raycast is hitting {0}", hit.collider.name));
        }

		timer = 0f;
	}

	void disableEffects() {
		gunLine.enabled = false;
	}

	public void grenade() {
		rifleObject.transform.SetParent(leftHand.transform, false);
	}

	public void grenadeFinished() {
		rifleObject.transform.SetParent(null);
        rifleObject.transform.position = Vector3.zero;
        rifleObject.transform.rotation = Quaternion.identity;
        rifleObject.transform.SetParent(rightHand.transform, false);
	}

	public string getWeaponName() {
		return currentWeapon.name;
	}

	public int getAmmo() {
		return currentWeapon.ammo;
	}
	
	public void damageBottle(int multiplier) {

		oldDamage = currentWeapon.damage;
		currentWeapon.damage *= multiplier;
		usingDamageBottle = true;
	}
}
