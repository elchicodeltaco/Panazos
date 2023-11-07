using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    private NavMeshAgent agente;
    private Transform granada;
    private Transform player;
    private Animator animator;


    private float attackDistance;
    public float alertRange;
    public float attackRange;


    public bool alertState;
    private bool attackState;

    private bool StillChasing;
    private bool StillAttacking;

    [SerializeField] LayerMask granadaMask;
    [SerializeField] LayerMask playersMask;

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
            //SFX
            if (!StillChasing)
            {
                int rand = Random.Range(1, 4);
                //ReproducirSFX(rand);
                StillChasing = true;
            }

            Chase();
        }*/
    }
    public void Chasing()
    {
        Vector3 lookPos;
        if (granada != null)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
