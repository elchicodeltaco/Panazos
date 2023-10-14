using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaControl : MonoBehaviour
{
    //Menu Pausa
    public GameObject MenuPausa;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if ((Time.timeScale != 0f))
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
            {
                Pausa();
            }
        }
        else
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            Reanudar();
        }
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        MenuPausa.SetActive(true);
        Cursor.visible = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooter>().enabled = false;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        MenuPausa.SetActive(false);
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooter>().enabled = true;
    }

    public void PausarMusica()
    {
        GameObject.Find("Audio").GetComponentInChildren<AudioSource>().Pause();
    }
    public void PlayMusica()
    {
        GameObject.Find("Audio").GetComponentInChildren<AudioSource>().Play();
    }

}
