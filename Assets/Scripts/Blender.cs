using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blender : MonoBehaviour
{
    public ParticleSystem Particulas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            
            //Particulas.gameObject.SetActive(true);
            //Particulas.Play();
        }
    }
}
