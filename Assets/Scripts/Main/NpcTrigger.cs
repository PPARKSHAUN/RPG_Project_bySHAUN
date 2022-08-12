using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTrigger : MonoBehaviour
{

    public GameObject canvas; // Ʈ���žȿ� ������ ���� ĵ���� 
    public GameObject questwindow;
    public GameObject[] button;
    public GameObject[] off; // �������� ������ ���� �͵� 
    public GameObject[] on; // �������������� ���ٰ͵�
    public AudioClip click;
    private void Start()
    {
      
    }


    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player" )
        {
                    canvas.SetActive(true);//Ʈ���žȿ� ������ ��ư Ȱ��ȭ 
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag == "Player")
        {
                    canvas.SetActive(false);//Ʈ���Ź���� ��Ȱ��ȭ
         }
    }
    public void Comunicationbutton()
    {
        SoundManger.instance.SFXPlay("Click", click);
        for(int i=0;i<CharacterManger.instance.myquests.Count;i++)
        {
            if(CharacterManger.instance.myquests[i].Questgoal.clearnpcid==GetComponentInParent<NpcData>().id)
            {
                CharacterManger.instance.myquests[i].isSucess = true;
            }
        }
    }
    public void AcceptButton()//������ư Ŭ�� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        if (CharacterManger.instance.myquests.Count==0)//ĳ���͸Ŵ����� ������Ʈ�� �ϳ��������� 
        {
            CharacterManger.instance.myquests.Add(QuestGiver.instance.quest[CharacterManger.instance.questchapter]);//������Ʈ�� �߰� 
            QuestGiver.instance.quest[CharacterManger.instance.questchapter].Progress = true;//����Ʈ ������ ture 
            questwindow.SetActive(false);//����Ʈ â ������ 
            for (int i = 0; i < button.Length; i++)//��ưâ �ٽú��̰� 
            {
                button[i].SetActive(true);
            }
        }
        else//�ƴҰ�� 
        {
            for (int i = 0; i < CharacterManger.instance.myquests.Count; i++)
            {
                if (CharacterManger.instance.myquests[i].goal == QuestGiver.instance.quest[CharacterManger.instance.questchapter].goal)//�̹̼����� ����Ʈ�ϰ�� 
                {
                    //�̹� ������ ����Ʈ ��� �˾� or �׳ɳ��α� 
                }

                else//�̹̼����� ����Ʈ�� �ƴҰ��  �ٵ� �̹� ���൵�� ����Ʈ�� �ޱ� ������ ��������ڵ�
                {
                    CharacterManger.instance.myquests.Add(QuestGiver.instance.quest[CharacterManger.instance.questchapter]);//������Ʈ�� �߰� 
                    QuestGiver.instance.quest[CharacterManger.instance.questchapter].Progress = true;//������ true 
                    questwindow.SetActive(false);//����Ʈâ ������ 
                    for (int j = 0; j < button.Length; j++)//��ưâ �ٽú��̰� 
                    {
                        button[j].SetActive(true);
                    }
                    break;
                }
            }
        }
        
       
    }
    public void RejectButton()//������ư Ŭ�� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        questwindow.SetActive(false);//����Ʈâ off 
        for(int i=0;i<button.Length;i++)
        {
            button[i].SetActive(true); //��ưâ on 
        }
        
    }

    public void OpenButtonClick() // ���� ��ư Ŭ���� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        
       for(int i=0;i<off.Length;i++) // ������Ұ͵� �迭�� �޾Ƽ� �ٲ��ְ� 
        {
            off[i].gameObject.SetActive(false); 
        }
        for (int i = 0; i < on.Length; i++) // ������Ұ͵� �迭�� �޾Ƽ� �ٲ��ְ� 
        {
            on[i].gameObject.SetActive(true);
        }
      


    }
    public void CloseButtonClick()//x��ư Ŭ���� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        
        for(int i=0;i<off.Length;i++)//�ٽ����ְ� 
        {
            off[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < on.Length; i++) // ������Ұ͵� �迭�� �޾Ƽ� �ٲ��ְ� 
        {
            on[i].gameObject.SetActive(false);
        }

    }
    public void purchaseButtonClick(int num)
    {
        SoundManger.instance.SFXPlay("Click", click);

        switch (num)
            {
                case 0:
                    Item myhp = GameManger.instance.MyItemList.Find(x => x.Name == "�ʺ��ڿ� HPȸ�� ����");//�������۸�Ͽ��� ���� ������ ã�� 
                    if(myhp == null)// null �̶�� 
                    {
                        GameManger.instance.MyItemList.Add(GameManger.instance.Alltem.Find(x=>x.Name== "�ʺ��ڿ� HPȸ�� ����")); // �������� ����Ʈ�� �߰����ְ� 
                    }
                    else // null�� �ƴϸ� 
                    {
                        int i = int.Parse(myhp.Number)+1; // �������� ����Ʈ hp ���� ���� intȭ ��Ų�� +1 
                        myhp.Number = i.ToString();// �������� ������ �־��ش� 
                    }
                   
                    break;
            case 1:
                Item mymp = GameManger.instance.MyItemList.Find(x => x.Name == "�ʺ��ڿ� MPȸ�� ����");//���Ͱ���
                if (mymp == null)
                {
                    GameManger.instance.MyItemList.Add(GameManger.instance.Alltem.Find(x => x.Name == "�ʺ��ڿ� MPȸ�� ����"));
                }
                else
                {
                    int i = int.Parse(mymp.Number) + 1;
                    mymp.Number = i.ToString();
                }
                break;
        }
        

      
        GameManger.instance.DrawQuickslot();//�����Կ� ��ϵǾ��������� ������ �����Ա׷��ֱ�
    }
   

  
}