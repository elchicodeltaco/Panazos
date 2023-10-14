using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerAnimatorCont : MonoBehaviour
{
    public void ShootFromAnim()
    {
        GetComponentInParent<PlayerShooter>().ShootToast();
    }

    public void DesactivateAttackAnimation()
    {
        GetComponent<Animator>().SetBool("Attack", false);
    }

    public void DesactivateDamageAnimation()
    {
        //GetComponent<Animator>().SetBool("Damage", false);

    }
}
