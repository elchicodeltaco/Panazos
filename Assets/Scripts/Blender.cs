using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blender : MonoBehaviour
{
    public ParticleSystem Particulas;
    [SerializeField] ZombieActivation m_Puerta;

    private EnemyBehavior m_ultimoZombie = null;
    private void OnTriggerEnter(Collider other)
    {
        if(m_Puerta != null)
        if (other.CompareTag("ZombiePelvis"))
        {
            EnemyBehavior otroombie = other.gameObject.GetComponentInParent<EnemyBehavior>();
            if(otroombie != m_ultimoZombie)
            {
                m_ultimoZombie = otroombie;
                m_Puerta.DeadZombie();
            }
        }
    }
}
