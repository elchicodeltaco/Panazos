using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheWay : MonoBehaviour
{

    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = GetComponent<Renderer>().material.color;
        ShowSolid();
    }

    public void ShowTransparent()
    {
        Color newColor = currentColor; // Crea una copia de currentColor
        newColor.a = 0.5f;
        GetComponent<Renderer>().material.color = newColor;
    }

    public void ShowSolid()
    {
        Color newColor = currentColor; // Crea una copia de currentColor
        newColor.a = 1f;
        GetComponent<Renderer>().material.color = newColor;
    }
}
