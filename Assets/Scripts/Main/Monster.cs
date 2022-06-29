using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum State
    {
        CREATE, IDLE, BATTLE, DIE,ROAMING
    }
    public float speed = 3f;
    public State myState = State.CREATE;
    public int maxHp;
    public int curHp;
    public int Damage;
    public Renderer myrenderer;
    public Animator myanim;
    public Vector3 pos;
    public LayerMask targetmask;
    Vector3 TargetPos = Vector3.zero;
    public Collider[] targetIndistance;
    public GameObject myTarget;
    public float Attackrange;
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
        targetIndistance = Physics.OverlapSphere(transform.position, 20f, targetmask);
        myTarget = targetIndistance[0].gameObject;
        if (myTarget != null)
        {
            ChangeState(State.BATTLE);
            
        }
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
    private void Update()
    {
       
    }
    IEnumerator Battle()
    {
        pos = myTarget.transform.position;
        myanim.SetBool("IsRunnig", true);
        while (true)
        {
            var dir = (pos - this.transform.position).normalized;
            this.transform.LookAt(pos);
            this.transform.position += dir * speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, pos);
            if(distance<= Attackrange)
            {
                if(myanim.GetBool("IsAttack")==false)
                {
                    myanim.SetTrigger("Attack");
                }
                

            }
            yield return null;
        }
    }

    IEnumerator Roaming()
    {
        

        pos = new Vector3(); //목적지 생성
        pos.x = Random.Range(-3f, 3f); //목적지 x 값은 -3~3 사이 랜덤값
        pos.z = Random.Range(-3f, 3f); // 목적지 z 값은 -3~3 사이 랜덤값
       

        myanim.SetBool("IsWalking", true); // 처음에 움직이니까 트루 
        while (true) 
        {
           
            var dir = (pos - this.transform.position).normalized; // pos 포지션 - 몬스터포지션 normalized 한후 
            this.transform.LookAt(pos); // 몬스터가 이동할때 이동하는곳 바라보게하기위해 
            this.transform.position += dir * speed * Time.deltaTime; // 현재포지션에 normalize * 설정한 speed * Time.deltaTime 더해주기 

            float distance = Vector3.Distance(transform.position, pos); // 몬스터와 목적지 사이 거리 구하기 
            if (distance <=0.1f) // 0.1 이하라면 
            {
                myanim.SetBool("IsWalking", false); // 걷는 모션 false 후 Idle 들어갈예정 
                yield return new WaitForSeconds(Random.Range(1f, 3f)); // 랜덤으로 1~3 초기다린후 
                myanim.SetBool("IsWalking", true); // 다시 걷게하고 
                pos.x = Random.Range(-3f, 3f);// 위치설정
                pos.z = Random.Range(-3f, 3f);//위치설정 
               

            }
            
            yield return null;  
        }

       
       
    

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
                ChangeState(State.ROAMING);
                break;
            case State.ROAMING:
                speed = 3f;
                StartCoroutine(Roaming());
                break;
            case State.BATTLE:
                speed = 5f;
                StopCoroutine(Roaming());
                StartCoroutine(Battle());
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
