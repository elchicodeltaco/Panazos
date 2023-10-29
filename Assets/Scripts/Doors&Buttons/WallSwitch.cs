using UnityEngine;
using UnityEngine.Events;

public class WallSwitch : ActivateDoor
{
    [SerializeField] private GameObject m_button;
    [SerializeField] private Color m_activeColor;
    private Renderer m_buttonMat;
    private Color m_normalColor;
    private bool activo;

    private void Start()
    {
        m_buttonMat = m_button.GetComponent<Renderer>();
        m_normalColor = m_buttonMat.material.color;
        activo= false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        activo = !activo;
        if (collision.gameObject.CompareTag("ToastBase"))
        {
            Color buttonColor = activo ? m_activeColor : m_normalColor;
            m_buttonMat.material.SetColor("_Color", buttonColor);
            Activate();
        }
    }
}
