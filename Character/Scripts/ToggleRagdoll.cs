using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRagdoll : MonoBehaviour
{
    public Animator animator;
    public bool enableRagdoll;
    public Vector3 newPosition;
    private bool ragdollEnabled;
    public CharacterController controller;

    // Use this for initialization
    void Start()
    {
        // Get components
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        // Disable or enable ragdoll
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = !enableRagdoll;
        }
        // Enable or disable animator
        animator.enabled = !enableRagdoll;
        // Enable or disable character controller
        controller.enabled = !enableRagdoll;
        // Set toggle flag
        ragdollEnabled = enableRagdoll;
    }

    // Update is called once per frame
    void Update()
    {
        if ((enableRagdoll && !ragdollEnabled) ||
            (!enableRagdoll && ragdollEnabled))
        {
            int count = 0;
            Vector3 p = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 v = new Vector3(0.0f, 0.0f, 0.0f);
            // Disable or enable ragdoll
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = !enableRagdoll;

                //Set ragdoll velocity to match character controller
                if (enableRagdoll) {
                    rb.velocity = controller.velocity;
                }

                //Add position and velocity to average
                p += rb.position;
                v += rb.velocity;
                count += 1;
            }
            p /= count;
            v /= count;

            // Move animator bones to current ragdoll position
            if (!enableRagdoll)
            {
                transform.position = p;
                newPosition = p;
            }

            // Enable or disable animator
            animator.enabled = !enableRagdoll;
            // Enable or disable character controller
            controller.enabled = !enableRagdoll;
            // Set toggle flag

            ragdollEnabled = enableRagdoll;
        }
    }
}


