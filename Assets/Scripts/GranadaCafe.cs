using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadaCafe : MonoBehaviour
{
    // public ParticleSystem dust;
    [SerializeField] private float tiempoEspera;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Granada";

    }


    IEnumerator Deshabilitar()
    {
        gameObject.tag = "Finish";
        yield return new WaitForSeconds(tiempoEspera);
        //ParticleSystem particle = Instantiate(dust);
        //particle.transform.position = transform.position;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Deshabilitar());

    }
}
