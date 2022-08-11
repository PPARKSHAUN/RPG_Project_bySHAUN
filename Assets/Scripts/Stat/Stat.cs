using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class Stat
{
    Stat stat = null;
    public UnitCode unitCode { get; set; } //유닛 코드는 바꿀 수 없게 Get만
    public string name { get; set; } //name 받고 셋팅 까지 할수있게 이하동문 
    public int maxHp { get; set; }
    public int curHp { get; set; }
    public int Damage { get; set; }
    public float moveSpeed { get; set; }
    public float AttackRange { get; set; }
    public int Level { get; set; }
    public int maxMp { get; set; }  
    public int curMp { get; set; }

    public Stat() 
    {

    }

    public Stat(UnitCode unitCode, string name, int maxHp, int Damage, float moveSpeed, float AttackRange,int Level,int maxMp, int curMp) // Stat 유닛코드,이름,체력,데미지,스피드,공격력 설정을위해 
    {
        this.unitCode = unitCode; // 입력받은 것들로 바꾸기위해 대입 이하동문
        this.name = name;
        this.maxHp = maxHp;
        curHp = maxHp;
        this.Damage = Damage;
       
        this.moveSpeed = moveSpeed;
        this.AttackRange = AttackRange;
        this.Level = Level;
        this.maxMp = maxMp;
        this.curMp = curMp;

    }

    public Stat SetUnitStat(UnitCode unitCode) // 유닛코드에 따라 설정해주기위해 
    {
        

        switch(unitCode) // 유닛코드 로 스위치 문 분별
        {
            case UnitCode.Warrior: // 유닛코드가 Warrior 면 
                stat = new Stat(unitCode,"Warrior",150,30,5f,3f,1,50,50); //유닛코드,이름,체력,데미지,사거리,이동스피드  이하동문
                break;
            case UnitCode.Fighter:
                stat = new Stat(unitCode, "Fighter", 130, 40, 6f, 3f,1,50,50);
                break;
            case UnitCode.Paladin:
                stat = new Stat(unitCode, "Paladin", 200, 15, 4.5f, 3f,1,50,50);
                break;
            case UnitCode.Archer:
                stat = new Stat(unitCode, "Archer", 100, 40, 6f, 15f, 1, 50, 50);
                break;
            case UnitCode.Wolf:
                stat = new Stat(unitCode, "Wolf", 200,10, 6f, 3f,1,0,0);
                break;
            case UnitCode.Dragon:
                stat = new Stat(unitCode, "Dragon", 1000, 30, 6f, 5f, 1, 0, 0);
                break;
              
        }
        return stat;
    }
    public Stat (JsonData json)
    {
        maxHp = int.Parse(json["maxHp"].ToString());
        curHp = int.Parse(json["curHp"].ToString());
        moveSpeed = int.Parse(json["moveSpeed"].ToString());
        unitCode = 0;
        Level = int.Parse(json["Level"].ToString());
        name = "Archer";

        AttackRange = int.Parse(json["AttackRange"].ToString());
        maxMp = int.Parse(json["maxMp"].ToString());
        curMp = int.Parse(json["curMp"].ToString());
        Damage = int.Parse(json["Damage"].ToString());
    
    }
    
}
