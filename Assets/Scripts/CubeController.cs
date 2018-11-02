using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    Camera theCamera;
    Transform playerTransform;

	// Use this for initialization
	void Start () {
        theCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {

        Ray ray = theCamera.ScreenPointToRay(Input.mousePosition);
        ray.direction.Normalize();

        float s = (Mathf.Abs(ray.direction.y) > 0) ? Mathf.Abs(theCamera.transform.position.y - playerTransform.position.y) / Mathf.Abs(ray.direction.y) : 0;
        Vector3 pos = theCamera.transform.position + ray.direction * s;


        transform.position = new Vector3(pos.x, playerTransform.position.y, pos.z);
	}
}
