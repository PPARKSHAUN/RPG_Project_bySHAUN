using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;

public class UserInfo : MonoBehaviour
{
    public string CharacterClass;
    public string nickname;
    public Text nicknameText;
    public string myclass;
    Param param = new Param();


    public override string ToString()
    {
        return $"nickname : {nickname}";
    }

    private void Update()
    {
        nickname = Backend.UserNickName;
        Debug.Log(nickname);
        nicknameText.text = nickname;
    }

    public void ToParam()
    {
        CharacterClass = DataManger.instance.curCharcter.ToString();

        param.Add("MyClass", CharacterClass);
        Backend.GameData.Insert("Character", param);

    }

    
    public void GetmyClass()
    {

       nickname = Backend.UserNickName;

        var bro = Backend.Social.GetUserInfoByNickName(nickname);
        JsonData json = bro.GetReturnValuetoJSON();
      

        var myclass = json["rows"][0]["MyClass"]["S"].ToString();
        var inDate = json["row"]["inDate"].ToString();
        Debug.Log(myclass);
        Debug.Log(inDate);
       
       
    }

   
      
}

 
   






