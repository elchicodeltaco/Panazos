using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform camFollower;
    public float camSpeed;

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
    public float RotationSpeed;

    //Variables para mecanica de brinco

    [Header("Gravity")]
    public float jumpHeigth;
    public float gravity = -9.81f;
    public Vector3 fallingvelocity;


    private Vector3 m_input;
    private Rigidbody rb;

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        m_input = transform.position;
    }


    void Update()
    {
        camFollower.position = Vector3.Lerp(camFollower.position, transform.position, camSpeed * Time.deltaTime);
        /*
        

        
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
        controller.Move(fallingvelocity * Time.deltaTime);*/
        GetInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        m_input = new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void Move()
    {
        rb.velocity = m_input * speed * Time.fixedDeltaTime;
        //A través de una esfera checamos si el personaje no está en el piso o cayendo
        isGrounded = Physics.CheckSphere(groundCkeck.position, groundDistance, groundMask);

        if (isGrounded && fallingvelocity.y < 0)
        {
            fallingvelocity.y = -8;
        }
        //gravity
        fallingvelocity.y += gravity * Time.deltaTime;
        rb.velocity += fallingvelocity* Time.deltaTime;
    }

    private void Look()
    {
        if (m_input != Vector3.zero)
        {

            Matrix4x4 matriz = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 skewedInput = matriz.MultiplyPoint3x4(m_input);

            Vector3 relative = (transform.position + skewedInput) - transform.position;
            Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, RotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCkeck.position, groundDistance);
    }
}
