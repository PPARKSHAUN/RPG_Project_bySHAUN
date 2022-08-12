using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    public GameObject prefab;
    public GameObject QuestlistWindow;
    public AudioClip click;
    public void ClickQuestList()//���� ui ���� ����Ʈ â �������� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        DrawList();// ����Ʈâ �׸��� 
        QuestlistWindow.SetActive(true);//����Ʈ���  Ȱ��ȭ 
    }
    public void ExitButtonclick()//x��ư Ŭ���� 
    {
        SoundManger.instance.SFXPlay("Click", click);
        QuestlistWindow.SetActive(false);//����Ʈâ ������ 
        DestroyList();// ����Ʈ ���������� 
    }
  public void DrawList()
    {
        for(int i=0;i<CharacterManger.instance.myquests.Count;i++)
        {
            GameObject go =Instantiate(prefab);//����� prefab �������� 
            go.transform.parent = this.transform;// �ڽ��� ��ġ 
            Text Title = transform.GetChild(i).GetComponent<Text>();//���� text �ֱ� 
            Text goal = Title.transform.GetChild(0).GetComponent<Text>();//goal text �ֱ� 
            Text state = goal.transform.GetChild(0).GetComponent<Text>();//���������� �Ϸ����� ���� �������� ���� 

            Title.text = CharacterManger.instance.myquests[i].title.ToString();// ĳ���� �Ŵ����� ������Ʈ ���� ����
            goal.text = CharacterManger.instance.myquests[i].goal.ToString();//ĳ���͸Ŵ��� ������Ʈ �� ���� 
            if(CharacterManger.instance.myquests[i].isSucess==false)//ĳ���� �Ŵ��� is sucess ���η� ���������� �Ϸ����� �ľ� 
            {
                state.text = "������";
            }
            else
            {
                state.text = "�Ϸ�";
            }
        }
    }
    public void DestroyList()// destroy��Ű�� �Լ� 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}