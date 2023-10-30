using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollEnabler : MonoBehaviour
{

    [SerializeField] protected Animator animator;
    [SerializeField] private Transform ragdollRoot;

    public Rigidbody[] rigidbodies;

    private void Awake()
    {
        rigidbodies = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        EnableAnimator();
        AwakeMethod();
    }


    public void EnableRagdoll()
    {
        animator.enabled = false;
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
        DeshabilitarComponentes();
    }

    public void EnableAnimator()
    {
        foreach (Rigidbody item in rigidbodies)
        {
            item.isKinematic = true;
            animator.enabled = true;
        }
    }

    public virtual void DesactivarAnimaciones() { }// override
    public virtual void DeshabilitarComponentes() { }// override

    public virtual void HabilitarComponentes() { } // override
    public virtual void AwakeMethod() { } // override


    public void Destruir()
    {
        Destroy(gameObject);
    }
}
