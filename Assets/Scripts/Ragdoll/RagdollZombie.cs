using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollZombie : RagdollEnabler
{
    [SerializeField] private MonoBehaviour[] scriptsToDesable;
    [SerializeField] private Collider[] collidersToDesable;
    [SerializeField] private Animator SonAnimator;
    [SerializeField] private float timeToWakeUp;
    [SerializeField] private float forceToRag;
    [SerializeField] private float multiplicationForce;
    [SerializeField] private Transform rootBone;
    [SerializeField] private ParticleSystem MoridoParticula;
    private bool ragdollActive;
    private Quaternion rootBoneRotation;


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
        StartCoroutine(RevivirRutina());
    }

    public override void HabilitarComponentes()
    {
        foreach (Collider collider in collidersToDesable)
        {
           collider.enabled = true;

        }
        foreach (MonoBehaviour script in scriptsToDesable)
        {
           script.enabled = true;
        }
        GetComponent<NavMeshAgent>().enabled = false;
    }
    IEnumerator RevivirRutina()
    {
        //Le decimos al game manager que murio un zombie
        //WaveManager.GetInstancia().ZombieAsesinadoMasacradoDestruidoXD();
        float rand = Random.Range(timeToWakeUp, timeToWakeUp + 2f);
        yield return new WaitForSeconds(rand);
        AlignPositionToRootBone();
        EnableAnimator();
        HabilitarComponentes();
                
    }

    public void AddForceToBones(Vector3 force)
    {
        rootBoneRotation = rootBone.rotation;
        EnableRagdoll();
        ragdollActive = true;
        foreach (Rigidbody rb in rigidbodies)
        {
            Vector3 temp = new Vector3(force.x, force.y + 20, force.z);
            rb.AddForce(temp * multiplicationForce);
        }
    }
    public void AddExplosionForceToBones(float force, Vector3 position,float radius)
    {
        rootBoneRotation = rootBone.rotation;
        EnableRagdoll();
        ragdollActive = true;
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddExplosionForce(force, position, radius);
        }
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
                AddForceToBones(force);
            }

            if (other.CompareTag("Blender") && ragdollActive)
            {

                Destroy(gameObject);
            }
        }
    }

    public void AlignPositionToRootBone()
    {
        //se guarda la posicion original del hueso de referencia
        Vector3 origialBonePos = rootBone.position;
        //se lleva todo el ojeto a la posicion en la que aterrizo el hueso
        transform.position = rootBone.position;
        
        //{Para checar que este en el suelo
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }

        //esto es para el espacio local de los huesos
        rootBone.position = origialBonePos;
        rootBone.rotation = rootBoneRotation;
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
            //print(force.magnitude);
            EnableRagdoll();
            foreach (Rigidbody rb in rigidbodies)
            {
                
                rb.AddForce(force * multiplicationForce);
            }

        }
    }
    public void destruirZombie()
    {

        ParticleSystem particulas = Instantiate(MoridoParticula);
        particulas.transform.position = gameObject.transform.position;
        particulas.Play();
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ZombieScript"))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<EnemyBehavior>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZombieScript"))
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<EnemyBehavior>().enabled = true;
        }
    }
}
