using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSword : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BackSword;
    public GameObject HandSword;
  public void Sword()
    {
        BackSword.SetActive(false);
        HandSword.SetActive(true);
    }
}
