using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlotonBehaviour : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private Transform m_parent;
    [SerializeField] private int m_maxEatenBread;
    [Header("Explosion Variables")]
    [SerializeField] private float m_delay = 3f;
    [SerializeField] private float m_explosionForce = 3f;
    [SerializeField] private float m_radius = 3f;
    private int m_eatenBread = 0;
    float m_countDown;
    bool m_explote = false;

    // Start is called before the first frame update
    void Start()
    {
        m_countDown = m_delay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explote()
    {
        //efecto de explosion

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_radius);

        foreach(Collider nearbyObj in colliders)
        {
            if (nearbyObj.CompareTag("Zombie"))
            {

                RagdollZombie rz = nearbyObj.GetComponent<RagdollZombie>();
                if (rz != null) print("iez");
                rz.AddExplosionForceToBones(m_explosionForce, transform.position, m_radius);
                
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ToastBase"))
        {
            if(m_eatenBread < m_maxEatenBread)
            {
                m_parent.localScale += new Vector3(0.1f,0.1f,0.1f);
                m_eatenBread++;
            }
        }
    }
}
