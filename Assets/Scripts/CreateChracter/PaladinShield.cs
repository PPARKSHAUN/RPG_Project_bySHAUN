using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinShield : MonoBehaviour
{
    public GameObject hand;
    public GameObject ground;
    public GameObject handsword;
    public GameObject swordeq;

    public void Shield()
    {
        ground.SetActive(false);
        hand.SetActive(true);
    }
    public void Sword()
    {
        swordeq.SetActive(false);
        handsword.SetActive(true);
    }
}
