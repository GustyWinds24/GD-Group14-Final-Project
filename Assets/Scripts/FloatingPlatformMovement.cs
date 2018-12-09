using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformMovement : MonoBehaviour {

    public Vector3 direction;
    public bool otherWay;
    public float timeBeforeSwitch = 0f;
    public bool hasReachedMaxWait;

    // Use this for initialization
    void Start () {
        hasReachedMaxWait = true;
        otherWay = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (hasReachedMaxWait == true)
        {
            if (otherWay == true)
            {
                transform.position -= direction;
            }
            else
            {
                transform.position += direction;
            }
        }
        else
        {
            timeBeforeSwitch += Time.deltaTime;
            if(timeBeforeSwitch >= 2f)
            {
                hasReachedMaxWait = true;
                timeBeforeSwitch = 0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = gameObject.transform;
            Debug.Log("Player On Platform");
        }
        if (collision.gameObject.CompareTag("TurnAroundBlock") && gameObject.CompareTag("FirstBlock"))
        {
            if (otherWay == true)
            {
                hasReachedMaxWait = false;
                otherWay = false;
            }
            else
            {
                hasReachedMaxWait = false;
                otherWay = true;
            }
            Debug.Log("TurnAroundBlock1touched");
        }
        if (collision.gameObject.CompareTag("TurnAroundBlock2") && gameObject.CompareTag("SecondBlock"))
        {
            if (otherWay == true)
            {
                hasReachedMaxWait = false;
                otherWay = false;
            }
            else
            {
                hasReachedMaxWait = false;
                otherWay = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("TurnAroundBlock2") && gameObject.CompareTag("SecondBlock"))
        {
            Debug.Log("platform hit block");
            if (otherWay == true)
            {
                hasReachedMaxWait = false;
                otherWay = false;
            }
            else
            {
                hasReachedMaxWait = false;
                otherWay = true;
            }
        }
    }

}
