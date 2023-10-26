using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject objetoAActivar;
    public bool Encendido;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ToastBase")){ 
            Debug.Log("AAAAAAAAAAAA");
            if (objetoAActivar.gameObject.CompareTag("Puerta"))
            {
                Encendido = !Encendido;
                if (Encendido)
                {
                    objetoAActivar.GetComponent<PuertaScript>().Activar();
                }
                else
                {
                    objetoAActivar.GetComponent<PuertaScript>().Desactivar();

                }
            }
        }
    }

}
