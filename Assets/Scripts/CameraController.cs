using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Transform playerTransform;
    Vector3 distance;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        distance = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = playerTransform.position + distance;
	}
}
