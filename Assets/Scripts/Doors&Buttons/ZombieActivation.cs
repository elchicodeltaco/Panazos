using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ZombieActivation : ActivateDoor
{
    [SerializeField]private int ZombiesToKill;
    public void DeadZombie()
    {
        ZombiesToKill--;
        if(ZombiesToKill<= 0)
        {
            Activate();
        }
    }
}

