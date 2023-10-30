using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PresureDoor : MonoBehaviour
{
    [SerializeField] PresureButton[] m_buttonsToPress;
    private int m_howManyButtons;

    private bool m_eventSended;

    [SerializeField] private UnityEvent m_activateTheDoors;

    // Start is called before the first frame update
    void Start()
    {
        m_howManyButtons = m_buttonsToPress.Length;
        m_eventSended = false;
    }

    // Update is called once per frame
    void Update()
    {
        int howManyAreActive = 0;
        foreach (PresureButton button in m_buttonsToPress)
        {
            if (button.m_active)
            {
                howManyAreActive++;
            }
        }
        if (howManyAreActive == m_howManyButtons)
        {
            if (!m_eventSended)
            {
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
        }
    }
}
