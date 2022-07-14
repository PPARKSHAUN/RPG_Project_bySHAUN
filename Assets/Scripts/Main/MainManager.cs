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
    public Image playersimbol;
    public Sprite[] Icon;
    RectTransform rectTransform;
    //public Text Uiclass;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }
    private void Start()
    {

        CreateCharacter();
        var bro = Backend.GameData.GetMyData("Character", new Where(), 10);
        for (int i = 0; i < bro.Rows().Count; ++i)
        {
            myclass = bro.Rows()[i]["MyClass"]["S"].ToString();
            // Debug.Log(myclass);
            //Uiclass.text = "("+myclass+")";
        }
        rectTransform = (RectTransform)playersimbol.transform;
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
                GameObject Archer = Instantiate(Resources.Load("Prefabs/Archer") as GameObject,StartPosition.position,StartPosition.rotation);
                rectTransform.sizeDelta=new Vector2(70,70);
                playersimbol.sprite = Icon[3];


                break;
            case "Paladin":
                Debug.Log("CreatePaladin");
                GameObject Paladin = Instantiate(Resources.Load("Prefabs/Paladin") as GameObject, StartPosition.position, StartPosition.rotation);
                playersimbol.sprite = Icon[1];
                break;
            case "Warrior":
                Debug.Log("CreateWarrior");
                GameObject Warrior = Instantiate(Resources.Load("Prefabs/Warrior") as GameObject, StartPosition.position, StartPosition.rotation);
                playersimbol.sprite = Icon[0];
                break;
            case "Fighter":
                Debug.Log("CreateFighter");
                GameObject Fighter = Instantiate(Resources.Load("Prefabs/Fighter") as GameObject, StartPosition.position, StartPosition.rotation);
                playersimbol.sprite = Icon[2];
                break;

        }
    }   
}
