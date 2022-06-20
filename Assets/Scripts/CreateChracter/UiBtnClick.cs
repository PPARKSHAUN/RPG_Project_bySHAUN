using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class UiBtnClick : MonoBehaviour
{
    public string CharacterClass;
    public InputField Nickname;
    public AudioClip clip;
    
    private bool CheckNickname()
    {
        return Regex.IsMatch(Nickname.text, "^[0-9a-zA-Z°¡-ÆR]*$");
    }
    public void BackButtonClick()
    {
       
        SoundManger.instance.SFXPlay("ButtonClick", clip);

        PickCharacter.instance.NotSelect();
        
        StartCoroutine(PickCharacter.instance.BackBtnClick());
      
    }
    
 
   
    public void CreateButtonClick()
    {
        CharacterClass = DataManger.instance.curCharcter.ToString();
       if(CheckNickname()==false)
        {
            Debug.Log("´Ð³×ÀÓÀº ÇÑ±Û,¿µ¾î,¼ýÀÚ·Î¸¸ ±¸¼º°¡´ÉÇÕ´Ï´Ù.");
            return;
        }

        BackendReturnObject BRO = Backend.BMember.CreateNickname(Nickname.text);

        if(BRO.IsSuccess())
        {
            Debug.Log("´Ð³×ÀÓ »ý¼º ¿Ï·á");
            LoadingManger.LoadScene("Main");
            UserInfo userInfo = new UserInfo();
            userInfo.ToParam();


        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "409":
                    Debug.Log("ÀÌ¹Ì Áßº¹µÈ ´Ð³×ÀÓÀÌ ÀÖ´Â °æ¿ì");
                    break;

                case "400":
                    if (BRO.GetMessage().Contains("too long")) Debug.Log("20ÀÚ ÀÌ»óÀÇ ´Ð³×ÀÓÀÎ °æ¿ì");
                    else if (BRO.GetMessage().Contains("blank")) Debug.Log("´Ð³×ÀÓ¿¡ ¾Õ/µÚ °ø¹éÀÌ ÀÖ´Â°æ¿ì");
                    break;

                default:
                    Debug.Log("¼­¹ö °øÅë ¿¡·¯ ¹ß»ý: " + BRO.GetErrorCode());
                    break;
            }
        }
    }
    
}
