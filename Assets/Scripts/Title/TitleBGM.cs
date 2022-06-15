using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    
    static AudioSource bgm;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        bgm = GetComponent<AudioSource>();  
    }

    public static void PlayMusic()
    {
        if (bgm.isPlaying) return;
        bgm.Play();
    }


    public static void StopMusic()
    {
        bgm.Stop();
    }
}
