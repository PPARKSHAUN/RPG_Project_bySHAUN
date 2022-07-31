using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTrigger : MonoBehaviour
{

    public GameObject canvas; // 트리거안에 왔을때 켜줄 캔버스 
    public GameObject questwindow;
    public GameObject[] button;
    public GameObject[] off; // 상점열기 했을때 꺼줄 것들 
    public GameObject[] on; // 상점열기했을때 켜줄것들

    private void Start()
    {
      
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player" )
        {
                    canvas.SetActive(true);//트리거안에 들어오면 버튼 활성화 
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag == "Player")
        {
                    canvas.SetActive(false);//트리거벗어나면 비활성화
         }
    }
    public void Comunicationbutton()
    {
        for(int i=0;i<CharacterManger.instance.myquests.Count;i++)
        {
            if(CharacterManger.instance.myquests[i].Questgoal.clearnpcid==GetComponentInParent<NpcData>().id)
            {
                CharacterManger.instance.myquests[i].isSucess = true;
            }
        }
    }
    public void AcceptButton()//수락버튼 클릭 
    {
        if(CharacterManger.instance.myquests.Count==0)//캐릭터매니저에 내퀘스트가 하나도없으면 
        {
            CharacterManger.instance.myquests.Add(QuestGiver.instance.quest[CharacterManger.instance.questchapter]);//내퀘스트에 추가 
            QuestGiver.instance.quest[CharacterManger.instance.questchapter].Progress = true;//퀘스트 진행중 ture 
            questwindow.SetActive(false);//퀘스트 창 꺼지게 
            for (int i = 0; i < button.Length; i++)//버튼창 다시보이게 
            {
                button[i].SetActive(true);
            }
        }
        else//아닐경우 
        {
            for (int i = 0; i < CharacterManger.instance.myquests.Count; i++)
            {
                if (CharacterManger.instance.myquests[i].goal == QuestGiver.instance.quest[CharacterManger.instance.questchapter].goal)//이미수락된 퀘스트일경우 
                {
                    //이미 수락된 퀘스트 라는 팝업 or 그냥냅두기 
                }

                else//이미수락된 퀘스트가 아닐경우  근데 이미 진행도로 퀘스트를 받기 때문에 상관없을코드
                {
                    CharacterManger.instance.myquests.Add(QuestGiver.instance.quest[CharacterManger.instance.questchapter]);//내퀘스트에 추가 
                    QuestGiver.instance.quest[CharacterManger.instance.questchapter].Progress = true;//진행중 true 
                    questwindow.SetActive(false);//퀘스트창 꺼지게 
                    for (int j = 0; j < button.Length; j++)//버튼창 다시보이게 
                    {
                        button[j].SetActive(true);
                    }
                    break;
                }
            }
        }
        
       
    }
    public void RejectButton()//거절버튼 클릭 
    {
        questwindow.SetActive(false);//퀘스트창 off 
        for(int i=0;i<button.Length;i++)
        {
            button[i].SetActive(true); //버튼창 on 
        }
        
    }

    public void OpenButtonClick() // 상점 버튼 클릭시 
    {
        CharacterManger.instance.CanMove = false;// 캐릭터 이동 막고 
       for(int i=0;i<off.Length;i++) // 꺼줘야할것들 배열로 받아서 다꺼주고 
        {
            off[i].gameObject.SetActive(false); 
        }
        for (int i = 0; i < on.Length; i++) // 꺼줘야할것들 배열로 받아서 다꺼주고 
        {
            on[i].gameObject.SetActive(true);
        }
      


    }
    public void CloseButtonClick()//x버튼 클릭시 
    {
        CharacterManger.instance.CanMove = true;//캐릭터 이동가능 
        for(int i=0;i<off.Length;i++)//다시켜주고 
        {
            off[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < on.Length; i++) // 꺼줘야할것들 배열로 받아서 다꺼주고 
        {
            on[i].gameObject.SetActive(false);
        }

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
