using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 pos = player.transform.position;
        pos.z += 6.0f;
        pos.x += 7.0f;
        pos.y += 12.0f;
        transform.position = pos;




    }
}
