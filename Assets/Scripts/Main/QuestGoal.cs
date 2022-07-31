using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goaltype;// 타입은 kill 과 comunication으로 나뉜다 
    public int requredAmount;//잡아야되는 숫자 
    public int curAmount;//현재까지 잡은수 
    public int clearnpcid;// 클리어 npc id 
    public bool IsReached()// 현재까지 잡은 몬스터 수가 잡아야되는 숫자보다 많으면 true 반환 
    {
        
        return (curAmount >= requredAmount);

       
    }
    public void EnemyKieeld()// 몬스터 죽는곳에 함수선언 
    {
        if(goaltype==GoalType.Kill)
        curAmount++;
      
    }
}

public enum GoalType
{
    Kill,
    Comunication
}
