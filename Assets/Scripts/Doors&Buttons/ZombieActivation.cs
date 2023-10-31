using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ZombieActivation : ActivateDoor
{
    [SerializeField] private int ZombiesToKill;
    [SerializeField] private TextMeshPro m_zombieTexto;
    private bool active;

    private void Start()
    {
        active = true;
        m_zombieTexto.text = ZombiesToKill.ToString();
    }
    public void DeadZombie()
    {
        if (active)
        {
            ZombiesToKill--;
            if (ZombiesToKill > 0)
            {
                m_zombieTexto.text = ZombiesToKill.ToString();
            }
            else
            {
                m_zombieTexto.text = ZombiesToKill.ToString();
                active = false;
                Activate();
            }
        }
    }
}

