using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPlayer : RagdollEnabler
{
    [SerializeField] private MonoBehaviour[] scriptsToDesable;
    [SerializeField] private Collider[] collidersToDesable;

    public override void DisableComponents()
    {
        foreach (MonoBehaviour script in scriptsToDesable)
        {
            script.enabled = false;
        }
        foreach (Collider collider in collidersToDesable)
        {
            collider.enabled = false;
        }
    }
}
