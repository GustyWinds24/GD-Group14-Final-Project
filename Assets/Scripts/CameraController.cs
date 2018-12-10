using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    

    Transform playerTransform;
    public float distance = 10f;
    public float angleFromGround = 45f;
    public float angleAround = 0f;
    public const float timeToAdjust = 0.5f;
    Vector3 currentVelocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = playerTransform.position + Quaternion.AngleAxis(angleFromGround, Vector3.right) * Quaternion.AngleAxis(angleAround, Vector3.up) * Vector3.back * distance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) return;
        transform.position = Vector3.SmoothDamp(transform.position, playerTransform.position + Quaternion.AngleAxis(angleFromGround, Vector3.right) * Quaternion.AngleAxis(angleAround, Vector3.up) * Vector3.back * distance, ref currentVelocity, timeToAdjust);
	}
}
