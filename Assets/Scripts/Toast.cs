using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "ToastBase";
        
    }


    IEnumerator Deshabilitar()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.tag = "Finish";
        yield return new WaitForSeconds(6.5f);
        ParticleSystem particle = Instantiate(dust);
        particle.transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);
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
        StartCoroutine(Deshabilitar());

    }
}
