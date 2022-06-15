using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleClickManger : MonoBehaviour
{
   
    public void TitleClick()
    {
        if(Input.GetMouseButtonUp(0))
        {
            LoadingManger.LoadScene("CreateCharacter");
        }
    }
}
