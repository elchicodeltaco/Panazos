using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateWander : Estado
{
    private ZombieBase m_zombie;
    private float lastMove;
    public float interval = 2;
    private bool destinationReached = false;

    //Se llama al constructor de la clase base
    public StateWander(MaquinaEstados fsm, Animator animator, ZombieBase zombie) : base(fsm, animator)
    {
        this.m_zombie = zombie;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("deambulando");
        animator.SetBool("Walk", true);
        animator.SetBool("Run", false);
    }
    public override void UpdateEstado()
    {
        if(m_zombie._getAlertState)
        {
            //m_zombie.m_agent.Stop();
            fsm.CambiarDeEstado(m_zombie.estadoChase);
        }
        if (Time.timeSinceLevelLoad >= lastMove + interval)
        {
            destinationReached = false;
            interval = Random.Range(2, 8);
        }
        //if (m_zombie.m_agent.remainingDistance <= m_zombie.m_agent.stoppingDistance) //done with path
        if (!destinationReached)
        {
            Vector3 point;
            if (RandomPoint(m_zombie.transform.position, m_zombie.m_wanderRange, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                destinationReached = m_zombie.m_agent.SetDestination(point);
            }
            lastMove = Time.timeSinceLevelLoad;
        }
        m_zombie.m_agent.speed = 1;
        m_zombie.m_agent.acceleration = 4;
        
        Debug.Log("mag " + m_zombie.m_agent.velocity.magnitude);
        
    }

    public override void Exit()
    {
        Debug.Log("exit deambular");
        animator.SetBool("Walk", false);
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
