using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveZombiesFromDoor : MonoBehaviour
{
    public ZombieActivation puertaReferida;
    // Start is called before the first frame update
    public void RemoverDePuerta()
    {
        if(puertaReferida != null)
        {
            puertaReferida.DeadZombie();
        }
    }
}
