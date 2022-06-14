using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBtnClick : MonoBehaviour
{
    public AudioClip clip;
    public void BackButtonClick()
    {
       
        SoundManger.instance.SFXPlay("ButtonClick", clip);
        StartCoroutine(PickCharacter.instance.BackBtnClick());
      
    }
}
