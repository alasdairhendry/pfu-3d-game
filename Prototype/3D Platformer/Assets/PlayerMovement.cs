using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Animator animator;
    private Rigidbody rb;
    [SerializeField] private float movementSpeed = 75.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isOnGroundPlatform = true;

	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        Jump();

        if (!isOnGroundPlatform)
            CheckGrounded();
    }

    // Update is called once per frame
    void FixedUpdate () {
        Movement();
	}

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            if (isGrounded)
                animator.SetBool("isWalking", true);
        }
        else if(h == 0 && v == 0)
        {
            animator.SetBool("isWalking", false);
        }

        if (h < 0)
        {
            if (isGrounded)
                animator.SetBool("isWalkingBack", true);
        }
        else
        {
            animator.SetBool("isWalkingBack", false);
        }

        if (isGrounded)
            rb.velocity = new Vector3(h, 0, v).normalized * movementSpeed * Time.fixedDeltaTime;        
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.VelocityChange);
            }
        }
    }

    private void CheckGrounded()
    {
        bool _isGrounded = false;

        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                Vector3 position = transform.position + new Vector3(i * 0.25f, 0.2f, j * 0.25f);
                Ray ray = new Ray(position, Vector3.down);
                RaycastHit hit;

                Debug.DrawRay(position, Vector3.down * 0.25f, Color.red, 0.15f);

                if (Physics.Raycast(ray, out hit, 0.25f))
                {                    
                    if (hit.collider.gameObject != null)
                    {
                        _isGrounded = true;
                        break;
                    }
                    else
                        _isGrounded = false;
                }
                else
                {
                    _isGrounded = false;
                }
            }

            if (_isGrounded)
                break;
        }

        isGrounded = _isGrounded;
    }

    public bool CheckOnPlatform(GameObject platform)
    {
        bool _isOnPlatform = false;

        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                Vector3 position = transform.position + new Vector3(i * 0.25f, 0.2f, j * 0.25f);
                Ray ray = new Ray(position, Vector3.down);
                RaycastHit hit;                

                if (Physics.Raycast(ray, out hit, 0.25f))
                {
                    if (hit.collider.gameObject == platform)
                    {
                        _isOnPlatform = true;
                        break;
                    }
                    else
                        _isOnPlatform = false;
                }
                else
                {
                    _isOnPlatform = false;
                }
            }

            if (_isOnPlatform)
                break;
        }

        return _isOnPlatform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckGrounded();

        isOnGroundPlatform = (collision.gameObject.tag == "GroundPlatform") ? true : false;
    }

    private void OnCollisionExit(Collision collision)
    {
        CheckGrounded();
        //Debug.Log("exit");
    }
}
