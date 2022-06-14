using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }

    public void SFXPlay(string sfxName , AudioClip clip)
    {
        GameObject Sound = new GameObject(sfxName+"Sound");
        AudioSource audiosource = Sound.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(Sound, clip.length);
    }
}
