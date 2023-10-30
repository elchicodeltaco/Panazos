using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehManager : MonoBehaviour
{
    private EnemyBehavior[] zombies;
    private void Start()
    {
        zombies = GetComponentsInChildren<EnemyBehavior>();
        foreach (EnemyBehavior enemy in zombies)
        {
            enemy.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        foreach (EnemyBehavior enemy in zombies)
        {
            enemy.enabled = true;
        }
    }
}
