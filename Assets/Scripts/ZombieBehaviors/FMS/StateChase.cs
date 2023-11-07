using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : Estado
{
    private ZombieBase m_zombie;
    private Vector3 m_normalVelocity;
    public StateChase(MaquinaEstados fsm, Animator animator, ZombieBase zombie) : base(fsm, animator)
    {
        this.m_zombie = zombie;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Persiguiendo");
        animator.SetBool("Run", true);
        m_normalVelocity = m_zombie.m_agent.velocity;
        m_zombie.m_agent.velocity *= 3;
    }
    public override void UpdateEstado()
    {
        //checar la esfera
        if (!fsm.mono.GetComponent<ZombieBase>()._getAlertState)
        {
            //m_zombie.m_agent.Stop();
            fsm.CambiarDeEstado(m_zombie.estadoWander);
        }
        //if (granada != null)
        //{
        //    lookPos = granada.position - transform.position;
        //    agente.enabled = true;
        //    agente.SetDestination(granada.transform.position);

        //}
        Debug.DrawRay(m_zombie.m_player.transform.position, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
        m_zombie.m_agent.SetDestination(m_zombie.m_player.transform.position);
        
        
        
    }
    public override void Exit()
    {
        Debug.Log("exit perseguir");
        animator.SetBool("Run", false);
        m_zombie.m_agent.velocity = m_normalVelocity;
    }
}