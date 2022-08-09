using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public static QuestGiver instance;
    public List<Quest> quest;
    public CharacterManger character;
    public GameObject[] button;
    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public GameObject[] rewardslot;
    public GameObject progerss;
    public GameObject sucess;
    public Sprite none;
    public AudioClip click;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }
    public void OpenQuestWindow()//npc ㅌ
    {
        SoundManger.instance.SFXPlay("Click", click);
            if(quest[CharacterManger.instance.questchapter].Progress==false&&quest[CharacterManger.instance.questchapter].isSucess==false)//quest[퀘스트진행도].progress,  issucees 가 false면 수락전 완료전 퀘스트 
            {
                questWindow.SetActive(true);//퀘스트 창 보이게 
            for(int i=0;i<button.Length;i++)//안보일 버튼들 추가해서 버튼들안보이게
            {
                button[i].SetActive(false);
            }
              
                titleText.text = quest[CharacterManger.instance.questchapter].title;//제목=quest[진행도].제목
                descriptionText.text = quest[CharacterManger.instance.questchapter].description;//설명=qeust[진행도].설명
                for (int j = 0; j < quest[CharacterManger.instance.questchapter].Rewarditems.Count; j++)//보상아이템 이미지는 
                {
                Debug.Log(j);
                    rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite = GameManger.instance.ItemSprite[GameManger.instance.Alltem.FindIndex
                        (x => x.Name == quest[CharacterManger.instance.questchapter].Rewarditems[j].Name)];//보상이템과 이름이같은 스프라이트 설정
                      if (quest[CharacterManger.instance.questchapter].Rewarditems[j]==null)
                {
                    rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite = none;
                }
                    if (rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite.name != "NONE")//none이 아닐때 활성화 
                    {
                        rewardslot[j].SetActive(true);
                    }
                }
            }
            else if(quest[CharacterManger.instance.questchapter].Progress==true&&quest[CharacterManger.instance.questchapter].isSucess==false)//qeust가 진행중일때 버튼을 진행중으로 보이게 
            {
                progerss.SetActive(true);
           
            questWindow.SetActive(true);
            for (int i = 0; i < button.Length; i++)
            {
                button[i].SetActive(false);
            }
            titleText.text = quest[CharacterManger.instance.questchapter].title;
            descriptionText.text = quest[CharacterManger.instance.questchapter].description;
            for (int j = 0; j < rewardslot.Length; j++)
            {
                rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite = GameManger.instance.ItemSprite[GameManger.instance.Alltem.FindIndex
                    (x => x.Name == quest[CharacterManger.instance.questchapter].Rewarditems[j].Name)];
                if (rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite.name != "NONE")
                {
                    rewardslot[j].SetActive(true);
                }
            }
        }
            else if(quest[CharacterManger.instance.questchapter].Progress == true && quest[CharacterManger.instance.questchapter].isSucess == true)//퀘스트가 진행중인데 성공했을때 버튼이 완료로 보이게 설정
        {
            questWindow.SetActive(true);
            for (int i = 0; i < button.Length; i++)
            {
                button[i].SetActive(false);
            }
            progerss.SetActive(false);
            sucess.SetActive(true);
            sucess.GetComponentInChildren<Text>().text = "완료";

        }
            
        }

    public void Sucess()//퀘스트 완료후 보상받을때 
    {
        SoundManger.instance.SFXPlay("Click", click);
        questWindow.SetActive(false);
        for(int i=0;i<rewardslot.Length;i++)
        {
            Item item = GameManger.instance.Alltem.Find(x => x.Name == quest[CharacterManger.instance.questchapter].Rewarditems[i].Name);
            if (quest[CharacterManger.instance.questchapter].Rewarditems[i].ItemType=="Potion")
            {
                Item Potion = GameManger.instance.MyItemList.Find(x => x.Name == item.Name);
                if (Potion != null)
                {
                    Potion.Number=item.Number+Potion.Number;
                }    
                else
                {
                    GameManger.instance.MyItemList.Add(item);
                }
            }
           
            else
            {
                GameManger.instance.MyItemList.Add(item);//그 아이템을 myitemlist에 추가 
            }
          
           

        }
        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(true);
        }
        for(int i=0;i< rewardslot.Length;i++)
        {
            rewardslot[i].transform.GetChild(1).GetComponent<Image>().sprite = none;
            rewardslot[i].gameObject.SetActive(false);
        }
        sucess.SetActive(false);
        sucess.GetComponentInChildren<Text>().text = "진행중";
        GameManger.instance.Save();
        CharacterManger.instance.myquests.Remove(CharacterManger.instance.myquests[CharacterManger.instance.questchapter]);
        CharacterManger.instance.questchapter += 1;//퀘스트 진행도 1 증가 
    }
       
    
    
}
