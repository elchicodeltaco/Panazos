using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    public ParticleSystem dust;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Coroutine m_deactiveCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    IEnumerator Deshabilitar()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.tag = "Finish";
        yield return new WaitForSeconds(6.5f);
        ParticleSystem particle = Instantiate(dust);
        particle.transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Zombie"))
        {
            gameObject.tag = "Finish";
        }
        if (!collision.gameObject.CompareTag("Blender"))
            m_deactiveCoroutine = StartCoroutine(Deshabilitar());
        if (collision.gameObject.CompareTag("Blender"))
        {
            if(m_deactiveCoroutine != null)
            StopCoroutine(m_deactiveCoroutine);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blender"))
        {
            //StopCoroutine(m_deactiveCoroutine);
            gameObject.SetActive(false);
        }
    }
}
