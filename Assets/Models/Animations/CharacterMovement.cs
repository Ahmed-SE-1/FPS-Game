using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;

    [Header("Movement Settings")]
    public float walkSpeed = 5f;      // Changed moveSpeed to walkSpeed to match logic
    public float sprintSpeed = 10f;
    public float gravity = 9.81f;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Get Input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. Check for movement direction
        Vector3 moveDir = transform.right * h + transform.forward * v;
        float moveAmount = Mathf.Abs(h) + Mathf.Abs(v);

        if (moveAmount > 0.1f)
        {
            // Default to walking
            float currentSpeed = walkSpeed;
            float animValue = 0.5f;

            // If holding Space, switch to sprinting
            if (Input.GetKey(KeyCode.Space))
            {
                currentSpeed = sprintSpeed;
                animValue = 1.0f;
            }

            // Move the character using the Controller
            controller.Move(moveDir * currentSpeed * Time.deltaTime);

            // Update the Animator parameter
            anim.SetFloat("Speed", animValue);
        }
        else
        {
            // Back to Idle
            anim.SetFloat("Speed", 0f);
        }

        // Apply gravity so you don't float away
        controller.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
    }
}