using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollZombie : RagdollEnabler
{
    [SerializeField] private MonoBehaviour[] scriptsToDesable;
    [SerializeField] private Collider[] collidersToDesable;
    [SerializeField] private Animator SonAnimator;
    [SerializeField] private float forceToRag;
    private bool ragdollActive;
    public override void DesactivarAnimaciones()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
    }

    public override void DeshabilitarComponentes()
    {
        foreach(MonoBehaviour script in scriptsToDesable)
        {
            script.enabled = false;
        }
        foreach(Collider collider in collidersToDesable)
        {
            collider.enabled = false;
        }
        GetComponent<NavMeshAgent>().enabled = false;
        StartCoroutine(MuertoRutina());
    }
    IEnumerator MuertoRutina()
    {
        //Le decimos al game manager que murio un zombie
        //WaveManager.GetInstancia().ZombieAsesinadoMasacradoDestruidoXD();

        yield return new WaitForSeconds(3);
        //animator.enabled = true;
        //DesactivarAnimaciones();
        SonAnimator.SetBool("Muerto", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToastBase"))
        {
            //SFX.
            //AudioManager.instancia.PlaySFX(9);

            Vector3 force = other.GetComponent<Rigidbody>().velocity;
            //print(force.magnitude);
            if (force.magnitude > forceToRag)
            {

                EnableRagdoll();
                ragdollActive = true;
                foreach (Rigidbody rb in rigidbodies)
                {
                    rb.AddForce(force * 55);
                }
            }

            if (other.CompareTag("Blender") && ragdollActive)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("DeathZone"))
        {
            EnableRagdoll();
        }*/
        if (collision.gameObject.CompareTag("ToastBase"))
        {
            //SFX.
            //AudioManager.instancia.PlaySFX(9);

            Vector3 force = collision.gameObject.GetComponent<Rigidbody>().velocity;
            print(force.magnitude);
            EnableRagdoll();
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.AddForce(force * 55);
            }

        }
    }
}
