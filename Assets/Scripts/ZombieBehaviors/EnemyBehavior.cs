using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agente;
    private float attackDistance;
    public float alertRange;
    public float attackRange;
    [SerializeField] LayerMask playersMask;
    [SerializeField] LayerMask granadaMask;

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
    private Transform granada;
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
        float distMin = alertRange;
        Collider[] granadas = Physics.OverlapSphere(transform.position, alertRange, granadaMask);
        


        if (granadas.Length > 0)
        {
            foreach (Collider granadaEnArreglo in granadas)
            {
                float distancia = Vector3.Distance(transform.position, granadaEnArreglo.transform.position);

                // Comprueba si esta granada está más cerca que la que ya tenías almacenada.
                if (distancia < distMin)
                {
                    distMin = distancia;
                    granada = granadaEnArreglo.transform;
                }
            }
        }
        else granada = null;


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
            /*
            Vector3 direction = player.position - transform.position;
            RaycastHit Hit;
            Debug.DrawRay(transform.position, direction);
            if (Physics.Raycast(transform.position, direction, out Hit, alertRange * 2))//, playersMask))
            {
                if(Hit.collider.gameObject.CompareTag("Player"))
                print("theres the player");
            }*/
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
        Vector3 lookPos;
        if(granada != null)
        {
            lookPos = granada.position - transform.position;
            agente.enabled = true;
            agente.SetDestination(granada.transform.position);

        }
        else
        {
            lookPos = player.position - transform.position;
            agente.enabled = true;
            agente.SetDestination(player.transform.position);
        }
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);



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
