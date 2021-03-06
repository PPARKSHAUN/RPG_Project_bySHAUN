using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;


public class UserInfo : MonoBehaviour
{
     public static UserInfo instance=null;
    public string CharacterClass;
    public string nickname;
    public Text nicknameText;
    public string myclass;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
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
        CharacterClass = DataManger.instance.curCharcter.ToString();
        Param param = new Param();
        param.Add("MyClass", CharacterClass);
        Backend.GameData.Insert("Character", param);

    }






}









