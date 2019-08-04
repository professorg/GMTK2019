using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip newMusic;
    public bool loop;

    void Awake()
    {
        Debug.Log("The music manager awakes");
        MusicPlayerSingleton ms = GameObject.FindWithTag("Music").GetComponent<MusicPlayerSingleton>();
        Debug.Log("Found: " + ms);
        ms.GetComponent<AudioSource>().clip = newMusic;
        ms.GetComponent<AudioSource>().loop = loop;
        ms.GetComponent<AudioSource>().Play(); 
    }

}
