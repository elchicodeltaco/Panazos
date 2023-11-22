using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosEfecto : MonoBehaviour
{ 
    public static SonidosEfecto instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void EjecutarSonido(AudioClip sonido)
    {
        audioSource.PlayOneShot(sonido);
    }
}
