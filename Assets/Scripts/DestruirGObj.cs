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
            parent.GetComponent<RemoveZombiesFromDoor>().RemoverDePuerta();
            other.GetComponent<Blender>().Particulas.transform.position = this.transform.position;
            other.GetComponent<Blender>().Particulas.gameObject.SetActive(true);
            other.GetComponent<Blender>().Particulas.Play();
        }
    }
}
