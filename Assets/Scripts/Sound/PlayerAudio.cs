using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip shoot;
    public AudioClip grenade;
    private List<AudioClip> m_clips = new List<AudioClip>();

    private void Start()
    {
        if (shoot != null)
        {
            m_clips.Add(shoot);
            if(grenade != null)
            m_clips.Add(grenade);
        }
    }

    public enum clips
    {
        shoot,
        grenade
    }

    public void PlayAudioClip(clips player)
    {
        if(m_clips[(int)player] != null)
        {
            GetComponent<AudioSource>().clip = m_clips[(int)player];
            GetComponent<AudioSource>().Play();
        }
    }
}
