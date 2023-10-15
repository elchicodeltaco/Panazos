using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PuertaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject puertaUno;
    public GameObject puertaDos;

    public Transform destinoPuertaUno;
    public Transform destinoPuertaDos;

    public Transform OriginalPosUno;
    public Transform OriginalPosDos;


    private bool estaActivo = false;

    public void Activar()
    {
        estaActivo = true;

    }
    public void Desactivar()
    {
        estaActivo = false;
    }

    private void destinoPuertas(Transform targetUno, Transform targetDos)
    {
        
        seek(targetUno, puertaUno);
        seek(targetDos, puertaDos);
    }

    private void seek(Transform target, GameObject puerta)
    {
        float step = Time.deltaTime * 2f;
        puerta.transform.position = Vector3.MoveTowards(puerta.transform.position, target.position, step);
    }

    private void Update()
    {
        if (estaActivo)
        {
            destinoPuertas(destinoPuertaUno, destinoPuertaDos);
        }
        else
        {
            destinoPuertas(OriginalPosUno, OriginalPosDos);
        }
    }
}
