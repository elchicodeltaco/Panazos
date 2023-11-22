using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerAparecer : MonoBehaviour
{
    public GameObject aparecer;
    private void Start()
    {
        aparecer.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aparecer.SetActive(true);
        }
    }
}
