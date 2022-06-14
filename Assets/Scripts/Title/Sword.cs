using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioClip clip;
    public void swordsound()
    {
        SoundManger.instance.SFXPlay("TitleSword", clip);
    }
}
