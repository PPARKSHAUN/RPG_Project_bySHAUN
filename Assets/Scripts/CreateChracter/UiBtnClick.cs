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
        return Regex.IsMatch(Nickname.text, "^[0-9a-zA-Z??-?R]*$");
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
            Debug.Log("?г????? ?ѱ?,????,???ڷθ? ?????????մϴ?.");
            return;
        }

        BackendReturnObject BRO = Backend.BMember.CreateNickname(Nickname.text);

        if(BRO.IsSuccess())
        {
           
            Debug.Log("?г??? ???? ?Ϸ?");
            LoadingManger.LoadScene("Main");
           
            UserInfo userInfo = new UserInfo();
            userInfo.ToParam();
           
          



        }
        else
        {
            switch (BRO.GetStatusCode())
            {
                case "409":
                    Debug.Log("?̹? ?ߺ??? ?г????? ?ִ? ????");
                    break;

                case "400":
                    if (BRO.GetMessage().Contains("too long")) Debug.Log("20?? ?̻??? ?г????? ????");
                    else if (BRO.GetMessage().Contains("blank")) Debug.Log("?г??ӿ? ??/?? ?????? ?ִ°???");
                    break;

                default:
                    Debug.Log("???? ???? ???? ?߻?: " + BRO.GetErrorCode());
                    break;
            }
        }
    }
    
}
