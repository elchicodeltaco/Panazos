using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{

    public GameObject objetoAActivar;

    public bool necesitaEstarPresionado;

    public LayerMask capaJugador;
    public LayerMask capaCaja;


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Caja"))
        {
            Debug.Log("Buenas");
            if (objetoAActivar.gameObject.CompareTag("Puerta"))
            {
                objetoAActivar.GetComponent<PuertaScript>().Activar();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){ 
            Debug.Log("AAAAAAAAAAAA");
            if (objetoAActivar.gameObject.CompareTag("Puerta"))
            {
                objetoAActivar.GetComponent<PuertaScript>().Activar();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("EEEEE");

            if (necesitaEstarPresionado)
            {
                if (objetoAActivar.gameObject.CompareTag("Puerta"))
                {
                    objetoAActivar.GetComponent<PuertaScript>().Desactivar();
                }
            }

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase"))
        {
            if (necesitaEstarPresionado)
            {
                if (objetoAActivar.gameObject.CompareTag("Puerta"))
                {
                    objetoAActivar.GetComponent<PuertaScript>().Desactivar();
                }
            }
            Debug.Log("chau");
        }

    }

}
