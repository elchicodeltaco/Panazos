using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    [SerializeField] private GameObject m_BaseObject;
    [SerializeField] private int m_Amount;

    private GameObject[] m_ObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        m_ObjectPool = new GameObject[m_Amount];

        for(int i = 0; i < m_Amount; i++)
        {
            GameObject temp = Instantiate(m_BaseObject);
            //temp.GetComponent<BoxCollider>().isTrigger = true;
            m_ObjectPool[i] = temp;
            m_ObjectPool[i].SetActive(false);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for(int i = 0; i < m_Amount; i++)
        {
            if (!m_ObjectPool[i].activeInHierarchy)
            {
                m_ObjectPool[i].SetActive(true);

                return m_ObjectPool[i];
            }
        }
        return null;
    }

}
