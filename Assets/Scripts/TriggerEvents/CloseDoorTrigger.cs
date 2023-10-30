using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseDoorTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent m_closingDoor;

    // Start is called before the first frame update
    void Start()
    {
        m_closingDoor.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_closingDoor.Invoke();
            Destroy(gameObject);
        }
    }
}
