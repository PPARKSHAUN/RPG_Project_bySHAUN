using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum State
    {
        CREATE, IDLE, BATTLE, DIE,ROAMING
    }
    public State myState = State.CREATE;
    public UnitCode unitCode; // 유닛코드 설정을위해 선언 
    public Stat stat; // 스탯을받아오기위해 
    public Renderer myrenderer;
    public Animator myanim;
    public LayerMask targetmask;
    public Collider[] targetIndistance;
    public GameObject myTarget;
    NavMeshAgent nav;
    Vector3 pos;
    public GameObject AttackPoint;
    CharacterManger characterManger;
    public Canvas myCanvas;
    Vector3 Startpos;
   
    private void Awake()
    {
        stat = new Stat();// 스탯 뉴 스탯 
        stat = stat.SetUnitStat(unitCode); // 스탯에 SetUnitStat 호출 (유닛코드로 구별하여) 유니티에서 wolf 로 설정해주면됨 
        ChangeState(State.ROAMING);
        nav = GetComponent<NavMeshAgent>();
         characterManger = GameObject.Find("Archer").GetComponent<CharacterManger>();
        Startpos = this.transform.position;
    }
    private void OnTriggerEnter(Collider other)//트리거엔터가 발동될때 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Arrow")) && myState != State.DIE)//엔터한 트리거에 레이어 이름이 Arrow 일때 
        {
           
            
            stat.curHp -= characterManger.stat.Damage;// 현재 hp 에서 캐릭터 데미지를 빼주고 
            StartCoroutine(OnDamage()); // ondamage 코루틴 시작 
            ChangeState(State.BATTLE); // 맞게되면 Battle 상태로 변경 
            myanim.SetTrigger("Damage");// 맞게되면 내 애니메이션 데미지 실행 
        }
        


    }
    IEnumerator OnDamage() // ondamage코루틴 
    {
        targetIndistance = Physics.OverlapSphere(transform.position, 20f, targetmask); // 20f 안에 player 타겟마스크 콜라이더 받기 
        myTarget = targetIndistance[0].gameObject; // player 는 하나밖에없기때문에 0 번을 내타겟으로설정 
        if (myTarget != null) // 내타겟이 null 이 아니면 
        {
            ChangeState(State.BATTLE); // 배틀모드로 넘어가게 
            
        }
        myrenderer.material.color= Color.red; // 몬스터 색깔 빨강으로 변경 
        yield return new WaitForSeconds(0.2f); // 0.2 초 기다린뒤 
        if(stat.curHp>0)// 현재 hp 가 0보다크다면 
        {
            myrenderer.material.color = new Color(1, 1, 1); //원래색깔로 
           
            characterManger.fixTarget = true; //만약 맞았는데 현재 hp 가 0보다크면  타겟 고정을 위해 스탑 코루틴 켜줄 bool 조정 
        }
        if (stat.curHp <= 0)//현재 hp 가 0보다 작다면
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


    IEnumerator Battle()
    {


        myanim.SetBool("IsRunnig", true); // 뛰어오는 애니메이션 재생을위해 
        while (true)
        {
            float distance = Vector3.Distance(transform.position, myTarget.transform.position); // 플레이어와 몬스터사이 거리 재기위함

            myanim.SetBool("IsWalking", false); // walking 이 true 로 안바뀌기위해 
            if (distance <= 20f && stat.curHp >0)
            {
                transform.LookAt(myTarget.transform);
            }

            nav.SetDestination(myTarget.transform.position); // 따라가게 하기위한 코드 
            if (myanim.GetBool("IsAttack") == true || myanim.GetBool("IsDamage") == true) // 공격애니메이션 이나 데미지애니메이션 둘중하나라도 재생중이라면 움직임이 멈추고
            {
                nav.isStopped = true;
            }
            else // 아니라면 움직여라
            {
                nav.isStopped = false;
            }



            if (distance <= stat.AttackRange) // 거리가 어택사거리 보다 짧거나 같다면 
            {
                if (myanim.GetBool("IsAttack") == false) // 공격중이아니라면 
                {
                    myanim.SetTrigger("Attack"); //공격애니메이션 재생 
                    StartCoroutine(Attack()); // 코루틴시작 
                }


            }

            if (distance >= 20f) // 몬스터와 캐릭터 거리 차이가 20f 가 넘으면 
            {

                myanim.SetBool("IsRunnig", false); // 애니메이션 런 false
                nav.isStopped = true;  // 네비 스탑 
                characterManger.fixTarget = false; // 캐릭터매니저 타겟고정 false 
                ChangeState(State.IDLE); // idle 상태로 변경 

            }

            if(stat.curHp<=0)
            {
               
               
                nav.isStopped = true;
            }






            yield return null;
        }
    }

    IEnumerator Attack() 
    {
        AttackPoint.SetActive(true); // 어택 콜리더 활성화 
        

        yield return new WaitForSeconds(1f);// 1초후 
        AttackPoint.SetActive(false);// 어택콜리더 비활성화 


}
    IEnumerator GoStartPos() // 시작위치로 돌아가는 코루틴 
    {

        myCanvas.transform.gameObject.SetActive(false); // 내 캔버스 안보이게 설정 
        myTarget = null; // 내 타겟 null 
        targetIndistance = null; // 콜리더 null 
      
        myanim.SetBool("IsWalking", true); // walk 애니메이션 true 
        while (true)
        {
            transform.LookAt(Startpos); // 시작포지션 바라보게 
            float dist = Vector3.Distance(transform.position, Startpos); // 시작포지션과 몬스터 distance

            this.transform.position = Vector3.MoveTowards(transform.position, Startpos, Time.deltaTime * stat.moveSpeed); // 시작위치로이동 

            if (dist <= 0.1f) // 시작 위치랑 몬스터의 거리가 0.1 f 보다 작다면 
            {
                myanim.SetBool("IsWalking", false);                // walk 애니메이션 false 
              
                ChangeState(State.ROAMING); // 로밍으로변경 

            }
            yield return null;
        }


    }
    IEnumerator Roaming()
    {
        
       
            pos = new Vector3(); //목적지 생성
            pos.x = Random.Range(-3f, 3f); //목적지 x 값은 -3~3 사이 랜덤값
            pos.y = 0.116f;
            pos.z = Random.Range(-3f, 3f); // 목적지 z 값은 -3~3 사이 랜덤값
            
        pos.x = this.transform.position.x-pos.x;
        pos.z = this.transform.position.z - pos.z;
      
       

        myanim.SetBool("IsWalking", true); // 처음에 움직이니까 트루 
        while (true) 
        {
           

            var dir = (pos - this.transform.position).normalized; // pos 포지션 - 몬스터포지션 normalized 한후 
            this.transform.LookAt(pos); // 몬스터가 이동할때 이동하는곳 바라보게하기위해 
            this.transform.position += dir * stat.moveSpeed * Time.deltaTime; // 현재포지션에 normalize * 설정한 speed * Time.deltaTime 더해주기 
            float distance = Vector3.Distance(transform.position, pos); // 몬스터와 목적지 사이 거리 구하기 
            if (distance <=0.1f) // 0.1 이하라면 
            {
                myanim.SetBool("IsWalking", false); // 걷는 모션 false 후 Idle 들어갈예정 
                yield return new WaitForSeconds(Random.Range(1f, 3f)); // 랜덤으로 1~3 초기다린후 
                myanim.SetBool("IsWalking", true); // 다시 걷게하고 
                pos.x = Random.Range(-3f, 3f);// 위치설정
                pos.z = Random.Range(-3f, 3f);//위치설정 
                pos.x = this.transform.position.x - pos.x;
                pos.z = this.transform.position.z - pos.z;

            }
            
            yield return null;  
        }

       
       
    

    }
    IEnumerator DropItem()
    {
        while(true)
        {

            yield return null;
        }
    }
    void ChangeState(State s) // State 변경 
    {

        if (myState == s) return; // 지금 내 state 가 입력받은 state 와 같다면 return 
        myState = s; // 아니라면 내 state 에 입력받은 s 대입 

        switch (myState)
        {
            case State.CREATE:
                break;
            case State.IDLE:
                
                StopAllCoroutines(); // 모든 코루틴을 멈추고 
                StartCoroutine(GoStartPos()); // 시작위치로 돌아가는 코루틴 시작 


                break;
            case State.ROAMING:
              

                StopAllCoroutines(); // 모든 코루틴을 멈추고 
                StartCoroutine(Roaming()); // 로밍 코루틴 시작 

                break;
            case State.BATTLE:
               
                StopAllCoroutines();

                StartCoroutine(OnDamage()); // ondamage 코루틴 시작 
                StartCoroutine(Battle()); // 스타트 코루틴 배틀 
                break;
            case State.DIE: // die 상태로돌입하면 
                StopAllCoroutines();
                myanim.SetBool("IsRunnig", false);
                CharacterManger characterManger = GameObject.Find("Archer").GetComponent<CharacterManger>();
                characterManger.fixTarget = false;// 만약 맞았는데 현재 hp 가 0보다작거나 같다면 타겟 세팅을 위해 스타트 코루틴 켜줄 bool 조정 
                myanim.SetTrigger("Dead"); // Dead 트리거 작동으로 Dead 애니메이션 나오고 
                StartCoroutine(Disapearing()); // Disapearing 코루틴 시작
               
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
