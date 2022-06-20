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
    UserInfo userInfo = new UserInfo();

    // Start is called before the first frame update
    void Start()
    {
        InitializeBackEnd();
        
    }

    public void ClickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(id.text, pw.text);
        if(BRO.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�.");
        }
    }

    public void ClickLogin()
    {
      
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, pw.text);
        if (bro.IsSuccess())
        {
            Debug.Log("�α��ο� �����߽��ϴ�");

            BackendReturnObject BRO = Backend.BMember.GetUserInfo();
            JsonData userInfoJson = BRO.GetReturnValuetoJSON()["row"];
            
            
          
           if (userInfoJson["nickname"] != null)
            {

                userInfo.GetmyClass();
             
                LoadingManger.LoadScene("Main");
                
                }
            else
            {
                LoadingManger.LoadScene("CreateCharacter"); //�α��� �����ϰ� �г����� null �̶�� ĳ���ͻ����� ����.
            }
            
        }
    }
    private void InitializeBackEnd() //�ڳ� �ʱ�ȭ 
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            //�ʱ�ȭ ���� �� ����
            Debug.Log("�ʱ�ȭ ����!");

        }
        else
        {
            Debug.LogError("�ʱ�ȭ ����!");
        }
    }

}