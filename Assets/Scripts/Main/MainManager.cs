using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public static MainManager instance;
    public string myclass;
    public Transform StartPosition;
    public GameObject[] rendercharacter;
    //public Text Uiclass;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    private void Start()
    {
        Destroy(GameObject.Find("BGM"));
        
        var bro = Backend.GameData.GetMyData("Character", new Where(), 10);
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            myclass = bro.Rows()[i]["MyClass"]["S"].ToString();
            // Debug.Log(myclass);
            //Uiclass.text = "("+myclass+")";
        }
      
        RenderCharacter();
        CreateCharacter();
    }


    public void RenderCharacter()
    {
        var bro = Backend.GameData.GetMyData("Character", new Where(), 10);
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            myclass = bro.Rows()[i]["MyClass"]["S"].ToString();
            // Debug.Log(myclass);
        }
        switch(myclass)
        {
            case "Archer":
                rendercharacter[0].SetActive(true);
                break;
            case "Paladin":
                rendercharacter[1].SetActive(true);
                break;
            case "Warrior":
                rendercharacter[2].SetActive(true);
                break;
            case "Fighter":
                rendercharacter[3].SetActive(true);
                break;
        }
    }
    public void CreateCharacter( )
    {
        var bro = Backend.GameData.GetMyData("Character", new Where(), 10);
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            myclass = bro.Rows()[i]["MyClass"]["S"].ToString();
            // Debug.Log(myclass);
        }
        switch (myclass)
        {
            case "Archer":
                Debug.Log("CreateArcher");
                GameObject Archer = Instantiate(Resources.Load("Prefabs/Archer") as GameObject,StartPosition.position,Quaternion.identity);
               


                break;
            case "Paladin":
                Debug.Log("CreatePaladin");
                GameObject Paladin = Instantiate(Resources.Load("Prefabs/Paladin") as GameObject, StartPosition.position, Quaternion.identity);
               
                break;
            case "Warrior":
                Debug.Log("CreateWarrior");
                GameObject Warrior = Instantiate(Resources.Load("Prefabs/Warrior") as GameObject, StartPosition.position, Quaternion.identity);
              
                break;
            case "Fighter":
                Debug.Log("CreateFighter");
                GameObject Fighter = Instantiate(Resources.Load("Prefabs/Fighter") as GameObject, StartPosition.position, Quaternion.identity);
               
                break;

        }
    }   
}
