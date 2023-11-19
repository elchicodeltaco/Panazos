using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blender : MonoBehaviour
{
    public ParticleSystem Particulas;
    [SerializeField] ZombieActivation m_Puerta;
    [SerializeField] AudioClip m_zombieMuerto;

    private ZombieBase m_ultimoZombie = null;
    private void OnTriggerEnter(Collider other)
    {
        if(m_Puerta != null)
        if (other.CompareTag("ZombiePelvis"))
        {
            ZombieBase otroombie = other.gameObject.GetComponentInParent<ZombieBase>();
                if (otroombie != m_ultimoZombie)
                {
                    print("c murio");
                    m_ultimoZombie = otroombie;
                    m_Puerta.DeadZombie();
                    SonidosEfecto.instance.EjecutarSonido(m_zombieMuerto);
                }
                else
                {
                    print("no cmurio");
                    //SonidosEfecto.instance.EjecutarSonido(m_zombieMuerto);

                }
        }
    }
}
