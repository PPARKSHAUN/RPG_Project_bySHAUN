using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;

public class BackEndManger : MonoBehaviour
{
    public InputField id;
    public InputField pw;
    
    public AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        InitializeBackEnd();
        
    }

    public void ClickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(id.text, pw.text);
        SoundManger.instance.SFXPlay("Click", click);
        if(BRO.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다.");
        }
    }

    public void ClickLogin()
    {
      
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, pw.text);
        SoundManger.instance.SFXPlay("Click", click);
        if (bro.IsSuccess())
        {
            Debug.Log("로그인에 성공했습니다");

            BackendReturnObject BRO = Backend.BMember.GetUserInfo();
            JsonData userInfoJson = BRO.GetReturnValuetoJSON()["row"];
            
            
          
           if (userInfoJson["nickname"] != null)
            {
               
                
                
                LoadingManger.LoadScene("Main");
                
                }
            else
            {
                LoadingManger.LoadScene("CreateCharacter"); //로그인 성공하고 닉네임이 null 이라면 캐릭터생성신 진입.
            }
            
        }
    }
    private void InitializeBackEnd() //뒤끝 초기화 
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            //초기화 성공 시 로직
            Debug.Log("초기화 성공!");

        }
        else
        {
            Debug.LogError("초기화 실패!");
        }
    }

}
