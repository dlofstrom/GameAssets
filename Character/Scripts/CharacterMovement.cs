using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Animator animator;
    public CharacterController controller;

    public bool idle;
    public bool crouch;
    public bool run;
    public bool air;

    public float gravity;
    public float jumpSpeed;
    private float velocityY;
    private float velocityX;
    public Vector3 velocity;
    public Vector3 velocity2;
    private bool enabledLast;
    public float rotationSpeed;
    public float moveSpeed;
    public float runFactor;
    public float crouchFactor;
    public ToggleRagdoll ragdoll;
    public float maxVelocity;
    public float absVel;
    public NearObjects nearObjects;


    public GameObject cube;
    public GameObject sphere;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        ragdoll = GetComponent<ToggleRagdoll>();
        nearObjects = GetComponent<NearObjects>();
        enabledLast = controller.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.enabled)
        {
            if (!enabledLast)
            {
                velocity += -controller.velocity;
                velocityY = 0;
            }
            velocity = transform.up * velocityY + transform.forward * velocityX;
            controller.Move(velocity);
            velocity2 = controller.velocity;

            if (controller.velocity.magnitude > maxVelocity &&
                ragdoll != null)
            {
                Debug.Log(ragdoll);
                ragdoll.enableRagdoll = true;
            }
        }

        if (controller.isGrounded)
        {
            animator.SetBool("air", false);
            air = false;
            velocityY = -0.01f;
        }
        else if (controller.enabled)
        {
            animator.SetBool("air", true);
            air = true;
            velocityY += Time.deltaTime * gravity;
        }


        if (controller.enabled)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("idle", false);
                idle = false;
                velocityX = Time.deltaTime * moveSpeed;
            }
            else
            {
                animator.SetBool("idle", true);
                idle = true;
                velocityX = 0.0f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(transform.up * rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(-transform.up * rotationSpeed);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetBool("crouch", true);
                crouch = true;
                velocityX *= crouchFactor;
            }
            else
            {
                animator.SetBool("crouch", false);
                crouch = false;
            }

            if (Input.GetKey(KeyCode.X))
            {
                animator.SetBool("run", true);
                run = true;
                velocityX *= runFactor;
            }
            else
            {
                animator.SetBool("run", false);
                run = false;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                velocityY += jumpSpeed;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                nearObjects.ConnectNear();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log(ragdoll);
                if (ragdoll != null)
                {
                    ragdoll.enableRagdoll = false;
                    Debug.Log("Disabled ragdoll");

                }
                else
                {
                    Debug.Log("Could not find ragdoll");
                }
            }
        }


        //Spawn objects
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject newGameObject = Instantiate(cube, transform.parent);
            newGameObject.transform.position = transform.position + 
                transform.up * 10.0f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject newGameObject = Instantiate(sphere, transform.parent);
            newGameObject.transform.position = transform.position + 
                transform.forward * 5.0f + 
                transform.up * 5.0f;
        }

        enabledLast = controller.enabled;

    }
}
