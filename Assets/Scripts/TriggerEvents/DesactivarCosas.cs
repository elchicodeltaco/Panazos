using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarCosas : MonoBehaviour
{
    public BoxCollider controlarTrigger;
    private bool activo = true;

    public void desactiver()
    {
        activo = !activo;
        controlarTrigger.enabled = activo;
    }
}
