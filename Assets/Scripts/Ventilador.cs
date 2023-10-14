using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilador : MonoBehaviour
{
    public float velocidad;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, velocidad);
    }
}
