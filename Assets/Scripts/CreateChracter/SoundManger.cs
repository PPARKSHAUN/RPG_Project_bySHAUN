using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManger : MonoBehaviour
{

    public AudioMixer mixer;
    
    public static SoundManger instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    public void SFXPlay(string sfxName , AudioClip clip)
    {
        GameObject Sound = new GameObject(sfxName+"Sound");
        AudioSource audiosource = Sound.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(Sound, clip.length);
    }
    public void SFX(float val)
    {
        mixer.SetFloat("SFX", Mathf.Log10(val) * 20);
    }

    
}
