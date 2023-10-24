using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public ParticleSystem dust;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    IEnumerator Deshabilitar()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.tag = "Finish";
        yield return new WaitForSeconds(6.5f);
        ParticleSystem particle = Instantiate(dust);
        particle.transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        rb.constraints = RigidbodyConstraints.None;

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.CompareTag("Escenario"))
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }*/
       

        if (collision.gameObject.CompareTag("Zombie"))
        {
            gameObject.tag = "Finish";
        }

        if (collision.gameObject.CompareTag("Piso"))
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        StartCoroutine(Deshabilitar());

    }

}
