using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
        {
            int damage = CharacterManger.instance.stat.Damage;
        }
    }
}
