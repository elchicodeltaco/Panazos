using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheWay : MonoBehaviour
{
    private Material m_solidMat;
    [SerializeField] private Material m_TransparentMat;
    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        currentColor = GetComponent<Renderer>().material.color;
        m_solidMat = GetComponent<MeshRenderer>().material;
        ShowSolid();
    }

    public void ShowTransparent()
    {
        //Color newColor = currentColor; // Crea una copia de currentColor
        //newColor.a = 0.5f;
        //GetComponent<Renderer>().material.color = newColor;

        GetComponent<MeshRenderer>().material = m_TransparentMat;
    }

    public void ShowSolid()
    {
        GetComponent<MeshRenderer>().material = m_solidMat;
        //Color newColor = currentColor; // Crea una copia de currentColor
        //newColor.a = 1f;
        //GetComponent<Renderer>().material.color = newColor;
    }
}
