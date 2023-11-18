using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotarCosas : MonoBehaviour
{
    public GameObject[] cosasAExplotar;
    public ParticleSystem explosiones;
    [SerializeField]private float seg = 3;

    public void Explotar()
    {
        StartCoroutine(ExplosionesParticulas());
        
    }
    public void ExpltotarFuncion()
    {
        Invoke("Explotar", seg);
    }

    private IEnumerator ExplosionesParticulas()
    {
        float interval = 0.2f;
        for (int i = 0; i < cosasAExplotar.Length; i++)
        {
            ParticleSystem part = Instantiate(explosiones);
            part.transform.position = cosasAExplotar[i].transform.position;
            yield return new WaitForSeconds(interval);
            cosasAExplotar[i].SetActive(false);
            interval *= 1.1f;
        }
    }
}
