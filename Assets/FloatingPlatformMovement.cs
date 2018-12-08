using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformMovement : MonoBehaviour {

    public Vector3 direction;
    public bool otherWay;

    // Use this for initialization
    void Start () {
        otherWay = false;
	}

    // Update is called once per frame
    void Update()
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
                otherWay = false;
            }
            else
            {
                otherWay = true;
            }
            Debug.Log("TurnAroundBlock1touched");
        }
        if (collision.gameObject.CompareTag("TurnAroundBlock2") && gameObject.CompareTag("SecondBlock"))
        {
            if (otherWay == true)
            {
                otherWay = false;
            }
            else
            {
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
                otherWay = false;
            }
            else
            {
                otherWay = true;
            }
        }
    }

}
