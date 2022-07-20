using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTrigger : MonoBehaviour
{

    public GameObject ShopCanvas; // 트리거안에 왔을때 켜줄 캔버스 
    public GameObject[] off; // 상점열기 했을때 꺼줄 것들 
    public GameObject on; // 상점열기했을때 켜줄것들
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player" )
        {
            ShopCanvas.SetActive(true);//트리거안에 들어오면 버튼 활성화 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ShopCanvas.SetActive(false);//트리거벗어나면 비활성화

         }
    }
    public void ShopOpenButtonClick() // 상점 버튼 클릭시 
    {
        CharacterManger.instance.CanMove = false;// 캐릭터 이동 막고 
       for(int i=0;i<off.Length;i++) // 꺼줘야할것들 배열로 받아서 다꺼주고 
        {
            off[i].gameObject.SetActive(false); 
        }

        on.SetActive(true);


    }
    public void ShopCloseButtonClick()//x버튼 클릭시 
    {
        CharacterManger.instance.CanMove = true;//캐릭터 이동가능 
        for(int i=0;i<off.Length;i++)//다시켜주고 
        {
            off[i].gameObject.SetActive(true);
        }
        on.SetActive(false);//캔버스 꺼주고 

    }
    public void purchaseButtonClick(int num)
    {
        
            switch (num)
            {
                case 0:
                    Item myhp = GameManger.instance.MyItemList.Find(x => x.Name == "초보자용 HP회복 포션");//내아이템목록에서 네임 같은걸 찾고 
                    if(myhp == null)// null 이라면 
                    {
                        GameManger.instance.MyItemList.Add(GameManger.instance.Alltem.Find(x=>x.Name== "초보자용 HP회복 포션")); // 내아이템 리스트에 추가해주고 
                    }
                    else // null이 아니면 
                    {
                        int i = int.Parse(myhp.Number)+1; // 내아이템 리스트 hp 포션 갯수 int화 시킨후 +1 
                        myhp.Number = i.ToString();// 내아이템 갯수에 넣어준다 
                    }
                   
                    break;
            case 1:
                Item mymp = GameManger.instance.MyItemList.Find(x => x.Name == "초보자용 MP회복 포션");//위와같음
                if (mymp == null)
                {
                    GameManger.instance.MyItemList.Add(GameManger.instance.Alltem.Find(x => x.Name == "초보자용 MP회복 포션"));
                }
                else
                {
                    int i = int.Parse(mymp.Number) + 1;
                    mymp.Number = i.ToString();
                }
                break;
        }
        

      
        GameManger.instance.DrawQuickslot();//퀵슬롯에 등록되어있을수도 있으니 퀵슬롯그려주기
    }
   

  
}
