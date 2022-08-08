using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniping : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameObject.FindWithTag("Player").GetComponent<CharacterManger>().myTarget.transform.position;
    }
}
