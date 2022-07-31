using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    public GameObject prefab;
    public GameObject QuestlistWindow;
    public void ClickQuestList()//메인 ui 에서 퀘스트 창 열었을때 
    {
        DrawList();// 퀘스트창 그리기 
        QuestlistWindow.SetActive(true);//퀘스트목록  활성화 
    }
    public void ExitButtonclick()//x버튼 클릭시 
    {
        QuestlistWindow.SetActive(false);//퀘스트창 꺼지게 
        DestroyList();// 퀘스트 지워버리기 
    }
  public void DrawList()
    {
        for(int i=0;i<CharacterManger.instance.myquests.Count;i++)
        {
            GameObject go =Instantiate(prefab);//연결된 prefab 을생성후 
            go.transform.parent = this.transform;// 자식의 위치 
            Text Title = transform.GetChild(i).GetComponent<Text>();//제목 text 넣기 
            Text goal = Title.transform.GetChild(0).GetComponent<Text>();//goal text 넣기 
            Text state = goal.transform.GetChild(0).GetComponent<Text>();//진행중인지 완료인지 상태 보기위해 대입 

            Title.text = CharacterManger.instance.myquests[i].title.ToString();// 캐릭터 매니저에 내퀘스트 제목 대입
            goal.text = CharacterManger.instance.myquests[i].goal.ToString();//캐릭터매니저 내퀘스트 골 대입 
            if(CharacterManger.instance.myquests[i].isSucess==false)//캐릭터 매니저 is sucess 여부로 진행중인지 완료인지 파악 
            {
                state.text = "진행중";
            }
            else
            {
                state.text = "완료";
            }
        }
    }
    public void DestroyList()// destroy시키는 함수 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
