using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum State
    {
        CREATE, IDLE, BATTLE, DIE,ROAMING
    }
    public State myState = State.CREATE;
    public int maxHp;
    public int curHp;
    public int Damage;
    public Renderer myrenderer;
    public Animator myanim;

    private void Awake()
    {
        ChangeState(State.IDLE);
       
    }
    private void OnTriggerEnter(Collider other)//트리거엔터가 발동될때 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Arrow")))//엔터한 트리거에 레이어 이름이 Arrow 일때 
        {
           
            Arrow arrow = other.GetComponent<Arrow>(); // Arrow 를 GetComponet 하고 
            curHp -= arrow.Damage;// 현재 hp 에서 arrow 데미지를 빼주고 
            StartCoroutine(OnDamage()); // ondamage 코루틴 시작 
            ChangeState(State.BATTLE); // 맞게되면 Battle 상태로 변경 
            myanim.SetTrigger("Damage");// 맞게되면 내 애니메이션 데미지 실행 
        }
        


    }
    IEnumerator OnDamage() // ondamage코루틴 
    {

        myrenderer.material.color= Color.red; // 몬스터 색깔 빨강으로 변경 
        yield return new WaitForSeconds(0.2f); // 0.2 초 기다린뒤 
        if(curHp>0)// 현재 hp 가 0보다크다면 
        {
            myrenderer.material.color = new Color(1, 1, 1); //원래색깔로 
            CharacterManger characterManger = GameObject.Find("Archer").GetComponent<CharacterManger>();
            characterManger.fixTarget = true; //만약 맞았는데 현재 hp 가 0보다크면  타겟 고정을 위해 스탑 코루틴 켜줄 bool 조정 
        }
        if (curHp <= 0)//현재 hp 가 0보다 작다면
        {
            ChangeState(State.DIE);
        }

    }
    IEnumerator Disapearing()//Disapearing 코루틴
    {
        
        myrenderer.material.color = Color.grey;// 몬스터 색깔 회색으로 변경
        yield return new WaitForSeconds(1.0f); // 1초뒤
       
        float dist = 1.0f; //1.0f 만큼 내려가게하려고 dist 설정
        while (dist > 0.0f)// dist 가 0보다 작아질때까지 무한루프
        {
            float delta = Time.deltaTime * 0.5f; 
            this.transform.Translate(-Vector3.up * delta); // 초당 0.5f 씩 떨어지게 
            dist -= delta; // dist 에서 빼준다 delta 만큼 
            yield return null;
        }
        Destroy(this.gameObject);// while 문 끝난후 몬스터 삭제 
    }

    void ChangeState(State s) // State 변경 
    {

        if (myState == s) return; // 지금 내 state 가 입력받은 state 와 같다면 return 
        myState = s; // 아니라면 내 state 에 입력받은 s 대입 

        switch(myState)
        {
            case State.CREATE:
                break;
            case State.IDLE:
                break;
            case State.ROAMING:
                break;
            case State.BATTLE:
                break;
            case State.DIE: // die 상태로돌입하면 
               
                myanim.SetTrigger("Dead"); // Dead 트리거 작동으로 Dead 애니메이션 나오고 
                StartCoroutine(Disapearing()); // Disapearing 코루틴 시작
                CharacterManger characterManger = GameObject.Find("Archer").GetComponent<CharacterManger>();
                characterManger.fixTarget = false;// 만약 맞았는데 현재 hp 가 0보다작거나 같다면 타겟 세팅을 위해 스타트 코루틴 켜줄 bool 조정 
                break;

        }
        
    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.CREATE:
                break;
            case State.IDLE:
                break;
            case State.ROAMING:
                break;
            case State.BATTLE:
                break;
            case State.DIE:
                
                break;

        }
    }

}
