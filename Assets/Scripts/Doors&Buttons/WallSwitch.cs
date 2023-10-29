using UnityEngine;
using UnityEngine.Events;

public class WallSwitch : MonoBehaviour
{
    [SerializeField] private GameObject m_button;
    [SerializeField] private Color m_activeColor;
    [SerializeField] private UnityEvent m_openTheDoor;

    private Renderer m_buttonMat;
    private Color m_normalColor;
    private bool m_active;


    // Start is called before the first frame update
    void Start()
    {
        m_buttonMat = m_button.GetComponent<Renderer>();
        m_normalColor = m_buttonMat.material.color;
        m_active = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase"))
        {
            m_active = !m_active;
            Color buttonColor = m_active ? m_normalColor : m_activeColor;
            m_buttonMat.material.SetColor("_Color", buttonColor);
            m_openTheDoor.Invoke();
        }
    }
}
