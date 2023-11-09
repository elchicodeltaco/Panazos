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
        DisableComponents();
    }

    public void EnableAnimator()
    {
        foreach (Rigidbody item in rigidbodies)
        {
            item.isKinematic = true;
            animator.enabled = true;
        }
        animator.Play("idle");
    }

    public virtual void DisableAnimations() { }// override
    public virtual void DisableComponents() { }// override

    public virtual void EnableComponents() { } // override
    public virtual void AwakeMethod() { } // override


    public void Destruir()
    {
        Destroy(gameObject);
    }
}
