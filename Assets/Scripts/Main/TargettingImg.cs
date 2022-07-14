using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargettingImg : MonoBehaviour
{
    public GameObject targetimg;
    public GameObject inventory;
    public Image hpbar; // fillamount 값 조정할거라 이미지 받아와주고 
    public GameObject HpBar; // hp바 프레임 배경 다가져오기위해 Gameobject로 받아주고 
    // Start is called before the first frame update
    void Start()
    {
        hpbar.fillAmount=(float)Monster.instance.stat.curHp/(float)Monster.instance.stat.maxHp; // 시작시 fillAmount = 몬스터 현제체력/몬스터 체력
    }

    // Update is called once per frame
    void Update()
    {
        if(inventory.activeSelf == false)
        {
            targetimg.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, (float)Monster.instance.stat.curHp / (float)Monster.instance.stat.maxHp, Time.deltaTime * 10);// 체력바가 부드럽게 줄기위해서 Lerp 를 사용하였다.
            HpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0));//몬스터 y축으로 2 더높게 뜨게 설정하였다.
        }
        
       
    }
}
