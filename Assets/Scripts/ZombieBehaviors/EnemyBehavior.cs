using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agente;


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
        //Behavior();
        
    }

    public void ReproducirSFX(int index)
    {
        //AudioManager.instancia.PlaySFX(index);
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
                agente.SetDestination(moveDirection);
                animator.SetBool("Walk", true);
                break;
        }
        fallingvelocity.y = -3f;
        zontroller.Move(fallingvelocity * Time.deltaTime);
    }


}
