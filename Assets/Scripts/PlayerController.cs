using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    [Header("Ground Stuff")]
    public Transform groundCkeck;
    public float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;

    public Transform camFollower;

    [Header("Rotation & Movement")]
    CharacterController controller;
    public float speed;
    private float smoothRotationTime = 0.1f;
    private float turnSmoothVel;

    public float camSpeed;

    public float gravity = -9.81f;
    private Vector3 fallingvelocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCkeck.position, groundDistance, groundMask);

        if (isGrounded && fallingvelocity.y < 0)
        {
            fallingvelocity.y = -2;
        }

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        float newVertical = vertical;
        float newHorizontal = horizontal;
        /*
        if (vertical == 1)
            newHorizontal++;

        if (horizontal == 1)
            newVertical--;

        if (vertical == -1)
            newHorizontal--;

        if (horizontal == -1)
            newVertical++;*/

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float varSpeed = speed;

        if (Input.GetButton("Fire3"))
        {
            varSpeed *= 2;
        }
        
            animator.SetFloat("Velocity", varSpeed * direction.magnitude);

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle +45f, ref turnSmoothVel, smoothRotationTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * new Vector3(0, 0, 1);

            controller.Move(moveDirection.normalized * varSpeed * Time.deltaTime);
        }

        //gravity
        fallingvelocity.y += gravity * Time.deltaTime;
        controller.Move(fallingvelocity * Time.deltaTime);

        camFollower.position = Vector3.Lerp(camFollower.position, transform.position, camSpeed * Time.deltaTime);
    }
}
