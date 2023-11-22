using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[SelectionBase]
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

    private Vector3 m_input;
    private float varSpeed;

    public bool caminar = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        camFollower.position = Vector3.Lerp(camFollower.position, transform.position, camSpeed * Time.deltaTime);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(3);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(4);

        }
        isGrounded = Physics.CheckSphere(groundCkeck.position, groundDistance, groundMask);

        varSpeed = speed;

        if (Input.GetButton("Fire3") || caminar)
        {
            varSpeed /= 2;
        }
    }

    private void Move()
    {
        animator.SetFloat("Velocity", varSpeed * m_input.magnitude);

        if (m_input.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(m_input.x, m_input.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle + 45f, ref turnSmoothVel, smoothRotationTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * new Vector3(0, 0, 1);

            controller.Move(moveDirection.normalized * varSpeed * Time.deltaTime);
        }

        if (!isGrounded && fallingvelocity.y < 0)
        {
            fallingvelocity.y = -8;
        }

        //gravity
        fallingvelocity.y += gravity * Time.deltaTime;
        controller.Move(fallingvelocity * Time.deltaTime);

    }
}
