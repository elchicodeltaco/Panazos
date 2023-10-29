using UnityEngine;
using UnityEngine.Events;

public class GroundButton : MonoBehaviour
{
    [SerializeField] private GameObject m_button;
    [SerializeField] private Color m_activeColor;
    [SerializeField] private float m_radius;
    [SerializeField] private LayerMask m_allowedLayers;
    [SerializeField] private UnityEvent m_activateTheDoors;


    private Renderer m_buttonMat;
    private Color m_normalColor;
    private bool m_eventSended;

    // Start is called before the first frame update
    void Start()
    {
        m_buttonMat = m_button.GetComponent<Renderer>();
        m_normalColor = m_buttonMat.material.color;
        m_eventSended = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] nerby = Physics.OverlapSphere(transform.position, m_radius, m_allowedLayers);
        if (nerby.Length > 0)
        {
            m_buttonMat.material.SetColor("_Color", m_activeColor);
            if (!m_eventSended)
            {
                print("todo ok");
                m_activateTheDoors.Invoke();
                m_eventSended = true;
            }
        }
        else
        {
            if (m_eventSended)
            {
                m_activateTheDoors.Invoke();
                m_eventSended = false;
            }
            m_buttonMat.material.SetColor("_Color", m_normalColor);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
