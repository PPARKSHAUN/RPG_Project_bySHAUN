using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
   public UnitCode unitCode { get; } //���� �ڵ�� �ٲ� �� ���� Get��
    public string name { get; set; } //name �ް� ���� ���� �Ҽ��ְ� ���ϵ��� 
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int Damage { get; set; }
    public float moveSpeed { get; set; }
    public float AttackRange { get; set; }

    public Stat() 
    {

    }

    public Stat(UnitCode unitCode, string name, int maxHp, int Damage, float moveSpeed, float AttackRange) // Stat �����ڵ�,�̸�,ü��,������,���ǵ�,���ݷ� ���������� 
    {
        this.unitCode = unitCode; // �Է¹��� �͵�� �ٲٱ����� ���� ���ϵ���
        this.name = name;
        this.maxHp = maxHp;
        curHp = maxHp;
        this.Damage = Damage;
       
        this.moveSpeed = moveSpeed;
        this.AttackRange = AttackRange;

    }

    public Stat SetUnitStat(UnitCode unitCode) // �����ڵ忡 ���� �������ֱ����� 
    {
        Stat stat = null;

        switch(unitCode) // �����ڵ� �� ����ġ �� �к�
        {
            case UnitCode.Warrior: // �����ڵ尡 Warrior �� 
                stat = new Stat(unitCode,"Warrior",150,30,5f,3f); //�����ڵ�,�̸�,ü��,������,��Ÿ�,�̵����ǵ�  ���ϵ���
                break;
            case UnitCode.Fighter:
                stat = new Stat(unitCode, "Fighter", 130, 40, 6f, 3f);
                break;
            case UnitCode.Paladin:
                stat = new Stat(unitCode, "Paladin", 200, 15, 4.5f, 3f);
                break;
            case UnitCode.Archer:
                stat = new Stat(unitCode, "Archer", 100, 50, 6f, 15f);
                break;
            case UnitCode.Wolf:
                stat = new Stat(unitCode, "Wolf", 200, 10, 6f, 3f);
                break;
        }
        return stat;
    }

}