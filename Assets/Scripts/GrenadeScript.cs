using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

    ParticleSystem emitter;
    AudioSource sound;
    Light grenadeLight;
    MeshRenderer rend;

	SphereCollider damageCollider;

	private void Awake() {
		damageCollider = GetComponents<SphereCollider>()[1];
	}

	// Use this for initialization
	void Start () {
        emitter = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();
        grenadeLight = GetComponent<Light>();
        rend = GetComponent<MeshRenderer>();
        StartCoroutine(Explode());
	}
	
	IEnumerator Explode()
    {
        yield return new WaitForSeconds(3f);
        rend.enabled = false;
        grenadeLight.intensity = 10;
        grenadeLight.color = Color.white;
        sound.Play();
        emitter.Play();

		damageCollider.enabled = true;

        yield return new WaitForSeconds(0.5f);
        grenadeLight.intensity = 0;
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Enemy")) {
			var e = other.gameObject.GetComponent<Enemy>();
			e.takeDamage(GameManager.instance.grenadeDamage);
		}
	}
}
