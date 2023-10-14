using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DDesOnLoad : MonoBehaviour
{
    private static DDesOnLoad instancia;

    public static DDesOnLoad GetInstancia()
    {
        return instancia;
    }

    private void Awake()
    {
        if (instancia == null)  // la variable que hace referencia al manager está asignada?
            instancia = this;   // si no, asígnala

        else if (instancia != this)  // ya estaba asignada, pero es otro objeto que no es este
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);  // hacemos que al cambiar de escena, el manager se mantenga

    }

    public int OleadaParaEmpezar = 10;

    
    public void manageWave(int number)
    {
        OleadaParaEmpezar += number;
        GameObject oleadas = GameObject.FindGameObjectWithTag("EditorOnly");
            
    }
}
