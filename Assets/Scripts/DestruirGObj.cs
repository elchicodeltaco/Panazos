using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirGObj : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blender"))
        {
            Destroy(parent);
        }
    }
}
