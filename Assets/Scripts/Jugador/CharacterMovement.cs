using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform cameraTransform;

    [Header("Ground Stuff")]
    public Transform groundCkeck;
    public float groundDistance;
    public LayerMask groundMask;
    private bool isGrounded;

    //movimiento general y suavizado de movimiento

    [Header("Rotation & Movement")]
    public float speed;
    private float smoothRotationTime = 0.1f;
    private float turnSmoothVel;

    //Variables para mecanica de brinco

    [Header("Gravity")]
    public float jumpHeigth;
    public float gravity = -9.81f;
    private Vector3 fallingvelocity;

    void Start()
    {

        //obtenemos el controlador del personaje

        controller = GetComponent<CharacterController>();
    }


    void Update()
    {

        //A través de una esfera checamos si el personaje no está en el piso o cayendo
        isGrounded = Physics.CheckSphere(groundCkeck.position, groundDistance, groundMask);

        if(isGrounded && fallingvelocity.y < 0)
        {
            fallingvelocity.y = -2;
        }



        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, smoothRotationTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            fallingvelocity.y = Mathf.Sqrt(jumpHeigth * -2f * gravity);
        }
        fallingvelocity.y += gravity * Time.deltaTime;
        controller.Move(fallingvelocity * Time.deltaTime);
    }
}
