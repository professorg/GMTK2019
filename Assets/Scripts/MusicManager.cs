using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip newMusic;
    public bool loop;

    void Awake()
    {
        //Debug.Log("The music manager awakes");
        MusicPlayerSingleton ms = GameObject.FindWithTag("Music").GetComponent<MusicPlayerSingleton>();
        //Debug.Log("Found: " + ms);
        AudioSource audio = ms.GetComponent<AudioSource>();
        if (audio.clip != newMusic) {
            audio.clip = newMusic;
            audio.Play(); 
        }
        audio.loop = loop;
    }

}
