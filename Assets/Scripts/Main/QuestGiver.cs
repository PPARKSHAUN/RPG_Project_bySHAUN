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
    public void OpenQuestWindow()//npc ��
    {
        SoundManger.instance.SFXPlay("Click", click);
        GameObject player = GameObject.FindWithTag("Player");
            if (quest[player.GetComponent<CharacterManger>().questchapter].Progress==false&&quest[player.GetComponent<CharacterManger>().questchapter].isSucess==false)//quest[����Ʈ���൵].progress,  issucees �� false�� ������ �Ϸ��� ����Ʈ 
            {
                questWindow.SetActive(true);//����Ʈ â ���̰� 
            for(int i=0;i<button.Length;i++)//�Ⱥ��� ��ư�� �߰��ؼ� ��ư��Ⱥ��̰�
            {
                button[i].SetActive(false);
            }
              
                titleText.text = quest[player.GetComponent<CharacterManger>().questchapter].title;//����=quest[���൵].����
                descriptionText.text = quest[player.GetComponent<CharacterManger>().questchapter].description;//����=qeust[���൵].����
                for (int j = 0; j < quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems.Count; j++)//��������� �̹����� 
                {
                Debug.Log(j);
                    rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("MainManager").GetComponent<GameManger>().ItemSprite[GameObject.Find("MainManager").GetComponent<GameManger>().Alltem.FindIndex
                        (x => x.Name == quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems[j].Name)];//�������۰� �̸��̰��� ��������Ʈ ����
                      if (quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems[j]==null)
                {
                    rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite = none;
                }
                    if (rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite.name != "NONE")//none�� �ƴҶ� Ȱ��ȭ 
                    {
                        rewardslot[j].SetActive(true);
                    }
                }
            }
            else if(quest[player.GetComponent<CharacterManger>().questchapter].Progress==true&&quest[player.GetComponent<CharacterManger>().questchapter].isSucess==false)//qeust�� �������϶� ��ư�� ���������� ���̰� 
            {
                progerss.SetActive(true);
           
            questWindow.SetActive(true);
            for (int i = 0; i < button.Length; i++)
            {
                button[i].SetActive(false);
            }
            titleText.text = quest[player.GetComponent<CharacterManger>().questchapter].title;
            descriptionText.text = quest[player.GetComponent<CharacterManger>().questchapter].description;
            for (int j = 0; j < quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems.Count; j++)
            {
                rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite = GameObject.Find("MainManager").GetComponent<GameManger>().ItemSprite[GameManger.instance.Alltem.FindIndex
                    (x => x.Name == quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems[j].Name)];
                if (rewardslot[j].transform.GetChild(1).GetComponent<Image>().sprite.name != "NONE")
                {
                    rewardslot[j].SetActive(true);
                }
            }
        }
            else if(quest[player.GetComponent<CharacterManger>().questchapter].Progress == true && quest[player.GetComponent<CharacterManger>().questchapter].isSucess == true)//����Ʈ�� �������ε� ���������� ��ư�� �Ϸ�� ���̰� ����
        {
            questWindow.SetActive(true);
            for (int i = 0; i < button.Length; i++)
            {
                button[i].SetActive(false);
            }
            progerss.SetActive(false);
            sucess.SetActive(true);
            sucess.GetComponentInChildren<Text>().text = "�Ϸ�";

        }
            
        }

    public void Sucess()//����Ʈ �Ϸ��� ��������� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        questWindow.SetActive(false);
        GameObject player = GameObject.FindWithTag("Player");
        GameObject MainManger = GameObject.Find("MainManager");
        for (int i=0;i < quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems.Count; i++)
        {
            Item item = quest[player.GetComponent<CharacterManger>().questchapter].Rewarditems[i];
            if (quest[player.GetComponent<CharacterManger>().questchapter].questid==1)
            {
                Item Potion = MainManger.GetComponent<GameManger>().MyItemList.Find(x => x.Name == item.Name);
                if (Potion != null)
                {
                    int number = int.Parse(Potion.Number) + int.Parse(item.Number);
                    Potion.Number=number.ToString();
                }    
                else
                {
                    MainManger.GetComponent<GameManger>().MyItemList.Add(item);
                }
            }
           
            else
            {
                MainManger.GetComponent<GameManger>().MyItemList.Add(item);//�� �������� myitemlist�� �߰� 
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
        sucess.GetComponentInChildren<Text>().text = "������";
      
      
        GameObject.FindWithTag("Player").GetComponent<CharacterManger>().myquests.Remove(CharacterManger.instance.myquests[CharacterManger.instance.questchapter]);
        GameObject.FindWithTag("Player").GetComponent<CharacterManger>().questchapter += 1;//����Ʈ ���൵ 1 ���� 
        //GameObject.Find("MainManager").GetComponent<GameManger>().Save();
        //GameObject.Find("MainManager").GetComponent<GameManger>().DrawQuickslot();
    }
       
    
    
}