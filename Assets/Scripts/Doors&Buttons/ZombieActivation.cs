using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ZombieActivation : ActivateDoor
{
    [SerializeField] private int ZombiesToKill;
    [SerializeField] private TextMeshPro m_zombieTexto;

    private void Start()
    {
        m_zombieTexto.text = ZombiesToKill.ToString();
    }
    public void DeadZombie()
    {
        if(ZombiesToKill>0)
        {
            ZombiesToKill--;
            m_zombieTexto.text = ZombiesToKill.ToString();
        }
        else
        {
            m_zombieTexto.text = ZombiesToKill.ToString();
            Activate();

        }
    }
}

