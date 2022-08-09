using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BGM : MonoBehaviour
{
    public static BGM instance;
    public  AudioMixer mixer;
    static AudioSource bgm;
    public AudioClip[] audioClips;
    public Slider slider;
    public float volum=1;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(transform.gameObject);
        bgm = GetComponent<AudioSource>();
        bgm.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        

    }
    public  void PlayMusic()
    {
       
        if (bgm.isPlaying) return;
        bgm.clip = audioClips[0];
        bgm.Play();
    }
    public void ChangeMusic(int i)
    {
        StopMusic();
        bgm.clip = audioClips[i];
        bgm.Play();
    }
    public void BGMvolmue(float val)
    {
        
        mixer.SetFloat("BGM", Mathf.Log10(val) * 20);
        volum =  val;
    }

    public  void StopMusic()
    {
        bgm.Stop();
    }
}
