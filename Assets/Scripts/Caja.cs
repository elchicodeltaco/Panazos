using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{

    public GameObject objetoAActivar;

    public LayerMask capaJugador;
    public LayerMask capaCaja;


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Caja"))
        {
            Debug.Log("Buenas");

            objetoAActivar.GetComponent<PuertaScript>().Activar();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){ 
            Debug.Log("AAAAAAAAAAAA");
            objetoAActivar.GetComponent<PuertaScript>().Activar();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("EEEEE");
            objetoAActivar.GetComponent<PuertaScript>().Desactivar();
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase"))
        {

            Debug.Log("chau");
            objetoAActivar.GetComponent<PuertaScript>().Desactivar();

        }

    }

}
