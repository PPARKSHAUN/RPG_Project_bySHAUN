using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Quest
{
    public bool isSucess;//성공했는지확인여부
    public bool Progress;//진행중인지 확인여부
    public int questid;//퀘스트 id 
    
    public string goal;// 퀘스트 내용
    public string title;//퀘스트제목
    public string description;//퀘스트 설명 
    public List<Item> Rewarditems;//보상아이템
    public UnitCode UnitCode;//무슨 몬스터를 잡아야하는지 설정 
    public QuestGoal Questgoal;// 퀘스트타입 , 잡아야되는몬스터수 , 현재잡은몬스터수 , 클리어 npc id 
}
  