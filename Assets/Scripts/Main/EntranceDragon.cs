using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDragon : MonoBehaviour
{
    public GameObject on;
    public GameObject sphere;
    public GameObject off;
    public GameObject maincam;
    public GameObject Animcam;
    public Animator RockAnim;
    public AudioClip click;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            on.SetActive(true);
            off.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            on.SetActive(false);
            off.SetActive(true);
        }
    }


    public void yesbuttonclick()
    {
        SoundManger.instance.SFXPlay("Click", click);
        RockAnim.SetTrigger("RockMove");
 
        Animcam.SetActive(true);
        on.SetActive(false);
        sphere.SetActive(false);
    }

    public void nobuttionclick()
    {
        SoundManger.instance.SFXPlay("Click", click);
        on.SetActive(false);
        off.SetActive(true);
    }
}
