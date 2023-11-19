using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[SelectionBase]
public class RagdollZombie : RagdollEnabler
{
    [Header("For Bread Physics")]
    [SerializeField] [Range(1, 3)] private int m_timeToWakeUp;
    [SerializeField] [Range(0f, 1f)] private float m_launchAngle;
    [SerializeField] private int m_multiplicationForce;
    [SerializeField] private float forceToRag;

    [Header("Ragdoll")]
    [SerializeField] private Transform rootBone;
    [SerializeField] public Transform m_rootBonePosition;
    [SerializeField] private LayerMask m_groundLayer;
    [SerializeField] private Animator SonAnimator;
    [SerializeField] private ParticleSystem MoridoParticula;
    [SerializeField] private MonoBehaviour[] scriptsToDesable;
    [SerializeField] private Collider[] collidersToDesable;
    private bool ragdollActive;
    private Quaternion rootBoneRotation;
    [SerializeField] AudioClip zombieSonido;
    [SerializeField] AudioClip golpe;
    [SerializeField] List<AudioClip> zombieWanderSonido = new List<AudioClip>();




    public override void DisableAnimations()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
    }
    public override void DisableComponents()
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
    public override void EnableComponents()
    {
        foreach (Collider collider in collidersToDesable)
        {
           collider.enabled = true;

        }
        foreach (MonoBehaviour script in scriptsToDesable)
        {
           script.enabled = true;
        }
        GetComponent<NavMeshAgent>().enabled = true;
        ragdollActive = false;
    }
    IEnumerator RevivirRutina()
    {
        //Le decimos al game manager que murio un zombie
        //WaveManager.GetInstancia().ZombieAsesinadoMasacradoDestruidoXD();
        float rand = Random.Range(m_timeToWakeUp, m_timeToWakeUp + 2f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(CheckGround);
        //print("Aterrizo");
        yield return new WaitForSeconds(rand);
        AlignPositionToRootBone();
        EnableAnimator();
        EnableComponents();
                
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
        rootBone.position = m_rootBonePosition.position;
        rootBone.rotation = rootBoneRotation;
    }
    public void AddForceToBones(Transform breadPos)
    {
        rootBoneRotation = rootBone.rotation;

        //hacer las mates
        Vector3 direccion = new Vector3(-breadPos.forward.x, -breadPos.forward.y + m_launchAngle, -breadPos.forward.z);
        //print("si le dio");
        EnableRagdoll();
        ragdollActive = true;
        foreach (Rigidbody rb in rigidbodies)
        {
            //Vector3 temp = new Vector3(force.x, force.y + 20, force.z);
            rb.AddForce(direccion.normalized * m_multiplicationForce);
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
    private bool CheckGround()
    {
        //if (Physics.Raycast(rootBone.position, Vector3.down * 0.2f, out RaycastHit hit, m_groundLayer))
        Collider[] col = Physics.OverlapSphere(rootBone.position, 0.2f, m_groundLayer);
        if (col.Length>0)
        {
            return true;
        }
        return false;
    }
    
    private void activateSoundAttack()
    {
        //SonidosEfecto.instance.EjecutarSonido(zombieSonido);
    }
    private void activateSoundWander()
    {
        int rand = Random.Range(0, zombieWanderSonido.Count);
        float random = Random.Range(0, 100);
        if(random == 14 || random ==90)
        {
            SonidosEfecto.instance.EjecutarSonido(zombieWanderSonido[rand]);

        }

    }

    public void DestroyZombie()
    {
        ParticleSystem particulas = Instantiate(MoridoParticula);
        particulas.transform.position = gameObject.transform.position;
        particulas.Play();
        Destroy(gameObject);
    }

    #region triggers colliders y gizmos
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToastBase"))
        {
            if (other.GetComponent<Rigidbody>().velocity.magnitude > forceToRag)
            {
                AddForceToBones(other.transform);
                SonidosEfecto.instance.EjecutarSonido(golpe);
            }            
        }
        if (other.CompareTag("Blender") && ragdollActive)
        {

            Destroy(gameObject);
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("ZombieScript"))
    //    {
    //        GetComponent<NavMeshAgent>().enabled = false;
    //        GetComponent<EnemyBehavior>().enabled = false;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("ZombieScript"))
    //    {
    //        GetComponent<NavMeshAgent>().enabled = true;
    //        GetComponent<EnemyBehavior>().enabled = true;
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        
        //if (collision.gameObject.CompareTag("ToastBase"))
        //{
        //    //SFX.
        //    //AudioManager.instancia.PlaySFX(9);

        //    Vector3 force = collision.gameObject.GetComponent<Rigidbody>().velocity;
        //    //print(force.magnitude);
        //    if (force.magnitude > forceToRag)
        //    {
        //        AddForceToBones(force);
        //    }
        //}
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }

    #endregion
}
