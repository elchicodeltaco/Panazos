using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{


    public LayerMask capaJugador;
    public LayerMask capaCaja;


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase") || collision.gameObject.GetComponentInParent<GameObject>().CompareTag("Player"))
        {
            Debug.Log("Buenas");


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
