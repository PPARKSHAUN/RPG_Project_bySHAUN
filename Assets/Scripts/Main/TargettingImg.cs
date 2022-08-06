using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargettingImg : MonoBehaviour
{
    public static TargettingImg instance;
    public GameObject targetimg;
    public GameObject inventory;
    public Image hpbar; // fillamount 값 조정할거라 이미지 받아와주고 
    public GameObject HpBar; // hp바 프레임 배경 다가져오기위해 Gameobject로 받아주고 
    public Text Damage;//데미지 받기위한 텍스트 
    public GameObject canvas;//데미지 캔버스 
        // Start is called before the first frame update
    void Start()
    {
        if(instance==null)
        {
            instance = this;
        }
        hpbar.fillAmount=(float)Monster.instance.stat.curHp/(float)Monster.instance.stat.maxHp; // 시작시 fillAmount = 몬스터 현제체력/몬스터 체력
    }

    public void Damaging(float damage)// 다른스크립트 코루틴은 스타트가 안되기때문에 스타트 해줄 함수 선언 
    {
        StartCoroutine(OndamageText(damage)); //스타트 코루틴 
    }

    public IEnumerator OndamageText(float damage)
    {
        canvas.SetActive(true);//canvas 보이게 
        Damage.text = damage.ToString();  // 텍스트는 전달받은 데미지로 
        Damage.fontSize = 60;//데미지 폰트 60 으로설정 
        for(int i=Damage.fontSize;i>=30;i--) // 폰트가 30이 될때까지 for 문 작동
        {
            Damage.fontSize = i; // 데미지 폰트사이즈는 i
            yield return new WaitForFixedUpdate();//다음 fixedupdate 까지 기다림;
        }
        yield return new WaitForSeconds(1f);// 1.5초 뒤에
        canvas.SetActive(false);//canavs 안보이게 
    }
    // Update is called once per frame
    void Update()
    {
        if(inventory.activeSelf == false)
        {
            Damage.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1, 0));
            targetimg.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
            hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, (float)Monster.instance.stat.curHp / (float)Monster.instance.stat.maxHp, Time.deltaTime * 10);// 체력바가 부드럽게 줄기위해서 Lerp 를 사용하였다.
            HpBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 2, 0));//몬스터 y축으로 2 더높게 뜨게 설정하였다.
        }
        
       
    }
}
