using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{


    public LayerMask capaJugador;
    public LayerMask capaCaja;


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase") || collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Buenas");
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){ 
            Debug.Log("AAAAAAAAAAAA");
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("EEEEE");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase"))
        {

            Debug.Log("chau");
        }

    }
    // Update is called once per frame

}
