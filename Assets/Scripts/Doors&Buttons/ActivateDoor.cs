using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateDoor : MonoBehaviour
{

    [SerializeField] private UnityEvent m_openTheDoor;
    private bool m_active;
    // Start is called before the first frame update
    void Start()
    {

        m_active = true;
    }

    // Update is called once per frame
    public void Activate()
    {
        m_active = !m_active;
        m_openTheDoor.Invoke();
    }
}
