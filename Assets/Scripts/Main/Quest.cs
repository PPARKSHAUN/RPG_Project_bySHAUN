using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Quest
{
    public bool isSucess;//�����ߴ���Ȯ�ο���
    public bool Progress;//���������� Ȯ�ο���
    public int questid;//����Ʈ id 
    
    public string goal;// ����Ʈ ����
    public string title;//����Ʈ����
    public string description;//����Ʈ ���� 
    public List<Item> Rewarditems;//���������
    public UnitCode UnitCode;//���� ���͸� ��ƾ��ϴ��� ���� 
    public QuestGoal Questgoal;// ����ƮŸ�� , ��ƾߵǴ¸��ͼ� , �����������ͼ� , Ŭ���� npc id 
}
  