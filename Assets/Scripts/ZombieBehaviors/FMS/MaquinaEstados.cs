using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaquinaEstados
{
    private Estado currentState;
    public MonoBehaviour mono;

    public MaquinaEstados(MonoBehaviour mono)
    {
        this.mono = mono;
    }
    public void Iniciar(Estado inicial)
    {
        currentState = inicial;
        //al conocer un estado ejecutamos su funcion de enter
        currentState.Enter();
    }

    // [( ono:c)^2:c] x [( ono:c)^2:c]
    //(ono)^2 + 2(ono:c) + :c^2 
    public void Update()
    {
        currentState.UpdateEstado();
    }

    public void CambiarDeEstado(Estado siguienteEstado)
    {
        if (siguienteEstado != currentState)
        {
            currentState.Exit();
            siguienteEstado.Enter();
            currentState = siguienteEstado;
        }
    }
}
































































//3:
//:´C