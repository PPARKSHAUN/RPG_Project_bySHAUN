using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;


public class UserInfo : MonoBehaviour
{
     public static UserInfo instance;
    public string CharacterClass;
    public string nickname;
    public Text nicknameText;
    public string myclass;
    public string InDate;
    private void Awake()
    {
       // DontDestroyOnLoad(gameObject);
        if (instance == null) instance = this;
        else if (instance != null) return;
    }
    public override string ToString()
    {
        return $"nickname : {nickname}";
    }

    public  void Update()
    {
     
            StartCoroutine(GetNickName(nicknameText));
     
       
      
        
        if( nickname != null)
        {
            StopCoroutine(GetNickName(nicknameText));
        }
      
    }

    IEnumerator GetNickName(Text text)
    {
        while(text != null)
        {
            nickname = Backend.UserNickName;
            text.text = nickname;
            yield return null;
        }
    }
    public void ToParam()
    {
        string s = "false";
        CharacterClass = DataManger.instance.curCharcter.ToString();
        Param param = new Param();
        param.Add("MyClass", CharacterClass);
        param.Add("Save", s);
 
        var bro= Backend.GameData.Insert("Character", param);
        if(bro.IsSuccess())
        {
            InDate = bro.GetInDate();
            Debug.Log(bro.GetInDate());
        }

    }






}









