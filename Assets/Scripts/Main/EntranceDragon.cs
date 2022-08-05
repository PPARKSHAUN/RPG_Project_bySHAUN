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
        RockAnim.SetTrigger("RockMove");
 
        Animcam.SetActive(true);
        on.SetActive(false);
        sphere.SetActive(false);
    }

    public void nobuttionclick()
    {
        on.SetActive(false);
        off.SetActive(true);
    }
}
