using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blender : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            //Destroy(other.gameObject);
            Debug.Log(other.name);
            other.GetComponent<RagdollZombie>().destruirZombie();
            
        }
    }
}
