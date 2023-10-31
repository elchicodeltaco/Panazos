using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    [SerializeField] private GameObject m_BaseObject;

    [SerializeField] private GameObject m_BaseObjectDos;


    [SerializeField] private int m_Amount;

    [SerializeField] private int m_AmountObjetoDos;


    private GameObject[] m_ObjectPool;
    private GameObject[] m_ObjectPoolDos;


    // Start is called before the first frame update
    void Start()
    {
        m_ObjectPool = new GameObject[m_Amount];
        m_ObjectPoolDos = new GameObject[m_AmountObjetoDos];

        for (int i = 0; i < m_Amount; i++)
        {
            GameObject temp = Instantiate(m_BaseObject);
            //temp.GetComponent<BoxCollider>().isTrigger = true;
            m_ObjectPool[i] = temp;
            m_ObjectPool[i].SetActive(false);
        }

        for (int i = 0; i < m_AmountObjetoDos; i++)
        {
            GameObject temp = Instantiate(m_BaseObjectDos);
            //temp.GetComponent<BoxCollider>().isTrigger = true;
            m_ObjectPoolDos[i] = temp;
            m_ObjectPoolDos[i].SetActive(false);
        }
    }

    public GameObject GetObjectFromPool(string tipoDisparo)
    {
        for(int i = 0; i < m_Amount; i++)
        {
            if(tipoDisparo == "Pan")
            {
                if (!m_ObjectPool[i].activeInHierarchy)
                {
                    m_ObjectPool[i].SetActive(true);

                    return m_ObjectPool[i];
                }
            }
            else if (tipoDisparo == "Granada")
            {
                if (!m_ObjectPoolDos[i].activeInHierarchy)
                {
                    m_ObjectPoolDos[i].SetActive(true);

                    return m_ObjectPoolDos[i];
                }
            }
        }
        return null;
    }

}
