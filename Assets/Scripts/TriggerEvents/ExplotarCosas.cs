using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotarCosas : MonoBehaviour
{
    public GameObject cosasAExplotar;
    [SerializeField]private float seg = 3;

    public void Explotar()
    {
        cosasAExplotar.SetActive(false);
    }
    public void ExpltotarFuncion()
    {
        Invoke("Explotar", seg);
    }
}
