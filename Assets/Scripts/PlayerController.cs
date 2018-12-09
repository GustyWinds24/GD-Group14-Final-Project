using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Animator animator;
    private CharacterController chrContr;
    private float angle;
    private float rotVelocity;
    private float lastAngle;
    private float gravity;
    private float oldAnimationSpeed;
    private float fasterRunTimer = 0f;
    //private bool justStartedFalling = true;
    bool moving;
    bool crouching;
	bool throwing;

    bool jumping;
    bool jumpHasFinished;
    Vector3 jumpVector;

    Vector3 path;
    float cameraAngle;

    GameObject bullet;
    GameObject gunShotSound;
    GameObject gunShot2Sound;
    GameObject gun;
    GameObject grenadeProp;
    GameObject grenadeObj;

    GameObject rightHand;
    //GameObject leftHand;

    AudioSource stepsSound;
	RifleController rifleController;
    Transform cursorTransform;
    Transform cameraTransform;

	private void Awake() {
        oldAnimationSpeed = 1;
		animator = gameObject.GetComponent<Animator>();
        chrContr = gameObject.GetComponent<CharacterController>();
        grenadeProp = Resources.Load("Prefabs/GrenadeProp") as GameObject;
        grenadeObj = Resources.Load("Prefabs/Grenade") as GameObject;
		//leftHand = transform.GetChild(0).gameObject;
        rightHand = transform.GetChild(1).gameObject;

        stepsSound = GetComponent<AudioSource>();
		rifleController = gameObject.GetComponent<RifleController>();
	}

	// Use this for initialization
	void Start () {
       
        cursorTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Vertical");
        float v = Input.GetAxis("Horizontal");
        bool toss = Input.GetButtonDown("Fire2") && !throwing;
        bool jump = Input.GetButtonDown("Jump");
        crouching = Input.GetButton("Fire3");

        Vector3 cameraVector = cameraTransform.rotation * Vector3.forward; cameraVector = new Vector3(cameraVector.x, 0f, cameraVector.z);
        cameraAngle = Vector3.Angle(Vector3.forward, cameraVector);
        cameraAngle *= Vector3.Angle(Vector3.right, cameraVector)>90?-1:1;

		if (chrContr.isGrounded) {
			animator.SetBool("OnGround", true);
			if (jumpHasFinished) {
				jumping = false;
				jumpHasFinished = false;
				animator.SetBool("Jumping", false);
			}

			if (jump) {
				StartCoroutine(Jump());
			}
		} else {
			animator.SetBool("OnGround", false);
		}


		Vector3 targetPos = new Vector3(cursorTransform.position.x, 0f, cursorTransform.position.z);
        Vector3 currentPos = new Vector3(transform.position.x, 0f, transform.position.z);

        float nextAngle = Vector3.Angle(Vector3.forward, targetPos - currentPos);
        float anglePerp = Vector3.Angle(Vector3.right, targetPos - currentPos);
        nextAngle *= (anglePerp > 90f)?-1: 1;

        angle = Mathf.SmoothDampAngle(angle, nextAngle, ref rotVelocity, 0.1f);

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        
        path = Quaternion.AngleAxis(-angle + cameraAngle, Vector3.up) * new Vector3(v, 0f, h);
        if (path.magnitude > 0.1 && !crouching && chrContr.isGrounded) moving = true;
        else moving = false;

        animator.SetFloat("BlendX", path.x);
        animator.SetFloat("BlendY", path.z);
        animator.SetFloat("Rotation", Mathf.Clamp((angle - lastAngle)/11.25f + 0.5f, 0f, 1f));
        lastAngle = angle;

        if(toss)
        {
			throwing = true;
            StartCoroutine(Throw());
        }
        if(moving)
        {
            if(!stepsSound.isPlaying)
            {
                stepsSound.Play();
            }
        }
        else {
            stepsSound.Stop();
        }
        if(crouching)
        {
            animator.SetBool("Crouching", true);
        } else
        {
            animator.SetBool("Crouching", false);
        }
        if(jumping)
        {
            chrContr.Move(jumpVector * Time.deltaTime);
        } else
        {
            //animator.SetBool("Jumping", false);
        }

        if(animator.speed != oldAnimationSpeed)
        {
            fasterRunTimer += Time.deltaTime;
            if (fasterRunTimer >= 10)
            {
                animator.speed = oldAnimationSpeed;
                fasterRunTimer = 0f;
            }
        }
	}

    IEnumerator Jump()
    {
        animator.SetBool("Jumping", true);
        yield return new WaitForSeconds(0.25f);

        jumpVector =Quaternion.AngleAxis(cameraAngle, Vector3.up) * new Vector3(Input.GetAxis("Horizontal"), 1.5f, Input.GetAxis("Vertical")).normalized * 10f;
        jumping = true;

        yield return new WaitForSeconds(0.2f);
        jumpHasFinished = true;

    }

    IEnumerator Throw()
    {
        animator.SetBool("Throwing", true);

		rifleController.grenade();
        yield return new WaitForSeconds(0.3f);

        GameObject grenade = Instantiate(grenadeProp, Vector3.zero, Quaternion.identity);
        grenade.transform.SetParent(rightHand.transform, false);

        yield return new WaitForSeconds(1.6f);
        GameObject grenadeReal = Instantiate(grenadeObj, transform.position + (Vector3.up*1.8f) + transform.rotation * (Vector3.forward), Quaternion.identity);
        grenadeReal.GetComponent<Rigidbody>().AddForce(transform.rotation * new Vector3(0f, 300f, 300f));
        Destroy(grenade);


        yield return new WaitForSeconds(1.0f);

        rifleController.grenadeFinished();
        animator.SetBool("Throwing", false);
		throwing = false;
    }

        private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 500, 20), path.ToString());
        GUI.Label(new Rect(0, 20, 500, 20), cameraAngle.ToString());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("FirstBlock") || collider.gameObject.CompareTag("SecondBlock"))
        {
            gameObject.transform.parent = collider.gameObject.transform;
            Debug.Log("Player On Platform");
        }
        if (collider.gameObject.CompareTag("SecondBlock"))
        {
            gameObject.transform.parent = collider.gameObject.transform;
            Debug.Log("Player On Platform");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("FirstBlock") || collider.gameObject.CompareTag("SecondBlock"))
        {
            gameObject.transform.parent = null;
        }
        if (collider.gameObject.CompareTag("SecondBlock"))
        {
            gameObject.transform.parent = null;
        }
    }

    public void changeAnimationSpeed()
    {
        animator.speed = 2.5f;
    }
}
