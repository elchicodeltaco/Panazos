using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PresureButton : MonoBehaviour
{
    public bool m_active;

    [SerializeField] private GameObject m_button;
    [SerializeField] private Color m_activeColor;
    [SerializeField] private float m_radius;
    [SerializeField] private LayerMask m_allowedLayers;

    private Renderer m_buttonMat;
    private Color m_normalColor;

    // Start is called before the first frame update
    void Start()
    {
        m_buttonMat = m_button.GetComponent<Renderer>();
        m_normalColor = m_buttonMat.material.color;
    }


    // Update is called once per frame
    void Update()
    {
        Collider[] nerby = Physics.OverlapSphere(transform.position, m_radius, m_allowedLayers);
        if (nerby.Length > 0)
        {
            m_buttonMat.material.SetColor("_Color", m_activeColor);
            m_active = true;
        }
        else
        {
            m_active = false;
            m_buttonMat.material.SetColor("_Color", m_normalColor);
        }
    }
}
