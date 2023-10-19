using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agente;
    private float attackDistance;
    public float alertRange;
    public float attackRange;
    [SerializeField] LayerMask playersMask;
    public bool alertState;
    private bool attackState;

    private CharacterController zontroller;

    public float rotationSpeed;
    public float speed;
    private int rutine;
    private Animator animator;
    private Quaternion angle;
    private float degreese;

    private float lastMove;
    public float interval;

    private Transform player;

    private bool StillChasing;
    private bool StillAttacking;

    public float gravity = -9.81f;
    private Vector3 fallingvelocity;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastMove = Time.timeSinceLevelLoad;
        zontroller = GetComponent<CharacterController>();
        agente = GetComponent<NavMeshAgent>();
        //WaveManager.GetInstancia().ZombieNuevoCreado();
    }

    // Update is called once per frame
    void Update()
    {
        if (!alertState)
            StillChasing = false;

        if (!attackState)
            StillAttacking = false;

        alertState = Physics.CheckSphere(transform.position, alertRange, playersMask);
        attackState = Physics.CheckSphere(transform.position, attackRange, playersMask);

        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);

        if (attackState)
        {
            //SFX
            if (!StillAttacking)
            {
                int rand = Random.Range(5, 8);
                //ReproducirSFX(rand);
                StillAttacking = true;
            }

            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            agente.enabled = false;
        }

        if (alertState)
        {
            //SFX
            if(!StillChasing)
            {
                int rand = Random.Range(1, 4);
                //ReproducirSFX(rand);
                StillChasing = true;
            }

            Chase();
        }
        else
        {
           Behavior();  
        }
        
    }

    public void ReproducirSFX(int index)
    {
        //AudioManager.instancia.PlaySFX(index);
    }

    public void Chase()
    {
        Vector3 lookpos = player.position - transform.position;
        lookpos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookpos);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);

        agente.enabled = true;
        agente.SetDestination(player.transform.position);

        animator.SetBool("Walk", false);
        animator.SetBool("Run", true);
    }

    public void Behavior()
    {
        agente.enabled = false;
        if(Time.timeSinceLevelLoad>= lastMove + interval)
        {
            rutine = Random.Range(0, 2);
            lastMove = Time.timeSinceLevelLoad;
        }
        switch (rutine)
        {
            case 0: 
                animator.SetBool("Walk", false); break;
            case 1: 
                degreese = Random.Range(0, 360);
                angle = Quaternion.Euler(0, degreese, 0);
                rutine++; break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, rotationSpeed);
                Vector3 moveDirection = Quaternion.Euler(0f, degreese, 0f) * new Vector3(0, 0, 1);
                zontroller.Move(moveDirection.normalized * speed * Time.deltaTime);
                animator.SetBool("Walk", true);
                break;
        }
        fallingvelocity.y = -3f;
        zontroller.Move(fallingvelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


}
