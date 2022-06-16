using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;


public class UserInfo : MonoBehaviour
{
    public string nickname;
    public Text nicknameText;

    public override string ToString()
    {
        return $"nickname : {nickname}";
    }

    private void Update()
    {
       nickname = Backend.UserNickName;
        nicknameText.text = nickname;
    }



}
