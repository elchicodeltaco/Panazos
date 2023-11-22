using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //para usar funcion de leer escenas

public class Escenas : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = true;
    }
    public void CargarEscenaString(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void CargarEscenaInt(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    public void Quit()
    {
        Application.Quit();
    }


}
