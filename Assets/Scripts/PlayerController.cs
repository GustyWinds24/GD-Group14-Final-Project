using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    private CharacterController chrContr;
    private float angle;
    private float gravity;
    private bool justStartedFalling = true;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        chrContr = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool jumping = Input.GetButton("Jump");

        Vector3 path = new Vector3(h, 0f, v).normalized * 0.15f;

        animator.SetFloat("Speed", path.magnitude * 10f);
     
        if(path.magnitude != 0)
        {
            angle = Vector3.Angle(Vector3.forward, path);
            float angleRight = Vector3.Angle(Vector3.right, path);

            if(angleRight > 90f)
            {
                angle *= -1f;
            }
        }

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        if(chrContr.isGrounded)
        {
            gravity = 0f;
            //justStartedFalling = true;
            if(jumping)
            {
                gravity = -0.5f;
            }

        } else
        {

            gravity += 0.015f;
        }
        path += Vector3.down * gravity;
        
        chrContr.Move(path);
	}
}
