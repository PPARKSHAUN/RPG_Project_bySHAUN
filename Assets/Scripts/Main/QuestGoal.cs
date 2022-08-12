using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goaltype;// Ÿ���� kill �� comunication���� ������ 
    public int requredAmount;//��ƾߵǴ� ���� 
    public int curAmount;//������� ������ 
    public int clearnpcid;// Ŭ���� npc id 
    public bool IsReached()// ������� ���� ���� ���� ��ƾߵǴ� ���ں��� ������ true ��ȯ 
    {
        
        return (curAmount >= requredAmount);

       
    }
    public void EnemyKieeld()// ���� �״°��� �Լ����� 
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