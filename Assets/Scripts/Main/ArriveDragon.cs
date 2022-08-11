using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveDragon : MonoBehaviour
{
    public Animator rockanim;
    public GameObject RockAnimCam;
    public Animator dragonanim;
    public GameObject MainUi;
    public GameObject HpbarCanvas;
    public GameObject bosscanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            bosscanvas.SetActive(true);
            rockanim.SetTrigger("RockReturn");
            dragonanim.SetBool("Idle", true);
            MainUi.SetActive(true);
            RockAnimCam.SetActive(true);
            HpbarCanvas.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<CharacterManger>().meetboos = true;
        }
    }
}
