using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Archer, Warrior, Paladin, Fighter
}
public class DataManger : MonoBehaviour
{
    public static DataManger instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }

    public Character curCharcter;
   
}
