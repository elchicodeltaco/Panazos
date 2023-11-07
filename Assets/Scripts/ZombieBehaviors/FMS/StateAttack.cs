using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : Estado
{
    private ZombieBase m_zombie;

    public StateAttack(MaquinaEstados fsm, Animator animator, ZombieBase zombie) : base(fsm, animator)
    {
        this.m_zombie = zombie;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.LogWarning("Atacando");
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);

    }
    public override void UpdateEstado()
    {
        //checar la esfera
        if (!fsm.mono.GetComponent<ZombieBase>()._getAttackState)
        {
            fsm.CambiarDeEstado(m_zombie.estadoChase);
        }
    }
    public override void Exit()
    {
        Debug.Log("exit atacar");
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
    }
}
