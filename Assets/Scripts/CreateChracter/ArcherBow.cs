using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBow : MonoBehaviour
{
    public GameObject hand;
    public GameObject back;

    public void Bow()
    {
        back.SetActive(false);
        hand.SetActive(true);
    }
}
