using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CharacterManger : MonoBehaviour
{
    public enum State
    {
        CREATE, IDLE, BATTLE,DIE 
    }
    public State myState = State.CREATE;
    public Transform cam;
    public GameObject Backbow;
    public GameObject Handbow;
    float moveSpeed = 6f;
    float rotationSpeed = 5f;
    Rigidbody body;
    Animator myanim;
    Vector3 movement;
    public float angleRange = 160f;
    public float distance = 10f;
    public LayerMask targetmask;
    public LayerMask etcmask;
    public Collider[] targetIndistance;
    public List<GameObject> Targets = new List<GameObject>();
    public GameObject targettingimg;
    public GameObject myTarget;
    public float curtarget;
    public float targetTomyPosition;
    public bool CanMove=true;
    public GameObject arrow;
    public GameObject arrowpoint;
    public bool fixTarget; //불값을 이용해 타겟고정할예정 
    void Awake()
    {
        myanim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        CanMove = true;
      
    }

    private void Update()
    {
       
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (myState==State.BATTLE)
        {
            if(fixTarget==false)// 스타트코루틴
            {
                StartCoroutine(SetTartget());
            }
           else // 스탑코루틴 
            {
                StopCoroutine(SetTartget());
            }
        }
        
        if (myanim.GetBool("IsAttack") == false && CanMove == true)
        {
            move(h, v);
            turn(h, v);
        }

    }
    
    void move(float h, float v)
    {
       
        
        movement.Set(h, 0, v);
       
       if(h ==0 && v == 0 )
        {
            myanim.SetBool("IsMoving", false);
        }
       else if( myanim.GetBool("EqipBow") != true)
        {
            myanim.SetBool("IsMoving", true);
        }
        if (myanim.GetBool("EqipBow") != true)
        {
          
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            body.MovePosition(transform.position + movement);
        }
            
    }

    void turn(float h, float v)
    {
        if (h == 0 && v == 0 )
            return;
        else if(myanim.GetBool("EqipBow") != true)
        {
            Quaternion Rotation = Quaternion.LookRotation(movement);
            body.rotation = Quaternion.Slerp(body.rotation, Rotation, rotationSpeed * Time.deltaTime);
        }
        
    }
 
    public IEnumerator SetTartget()
    {
      
        Targets.Clear(); //배열에 계속쌓이는걸 방지하려고 클리어 하고 받기 
       targetIndistance = Physics.OverlapSphere(transform.position, distance, targetmask); // 내위치 중점, distance=사거리 구체반경,target마스크찾기위한거

        for (int i = 0; i < targetIndistance.Length; i++) //내사거리안에 몬스터만큼 돌려주려고 한거 
        {
            Transform target = targetIndistance[i].transform; //몬스터위치 

            Targets.Add(target.transform.gameObject);


        }
        
        if(Targets.Count != 0)//판별된 적이 1명이라도 있다면 실행되게 
        {

            if (targettingimg != null) //새로 타겟팅 됬을때 이미지를 초기화시켜주기위해 
                targettingimg.SetActive(false);

            myTarget = Targets[0];//첫번째 타겟을 나의타겟으로 넣어놓고 
            curtarget = Vector3.Distance(transform.position, Targets[0].transform.position);// 그 타겟과 나의 거리를 계산한뒤 

            for(int i=1;i<Targets.Count;i++) //for 문을 돌려주는데 i 의 0번째는 이미 들어갔기때문에 1번째부터 비교하기위해 i= 1 
            {
               
                float dist=Vector3.Distance(transform.position,Targets[i].transform.position);  //[i]번째 타겟과 내 거리를 계산하고
                if(dist<curtarget)// [i]번째 타겟의 거리가 현재타겟의 거리보다 작으면 실행
                {
                    myTarget=Targets[i]; //나의타겟은 [i]번째 타겟으로바뀌고
                    curtarget = dist; // 현재 타겟과의거리도 [i]번째 타겟의거리로 넣어준다.
                    
                }
               
            }

            targettingimg = myTarget.transform.Find("Canvas").transform.gameObject;// 나의 타겟에 캔버스를 찾아서 
            targettingimg.SetActive(true);// 캔버스를 보이게해준다.
        }
        else if(Targets.Count==0 && targettingimg != null)//만약 타겟이 없고 타겟팅이미지가 null이아니면 안보여지게하기위해 
        {
            targettingimg.SetActive(false);
        }

      yield return null;    
    }



   

    public void DrawArrow()
    {
        GameObject go = Instantiate(arrow, arrowpoint.transform.position, arrowpoint.transform.rotation);

    }
    public void ArrowRecoil()
    {
        
    }
    public void OnclickAttackButton()
    {
        if(myanim.GetBool("IsAttack")==false)//파라메터 IsAttack false 일때 실행
        {
            if (targetIndistance != null) // 첫클릭시 주위에 적이 있다면 
            {
                ChangeState(State.BATTLE); //배틀모드로 바꿔주고 

            }
            if (myTarget != null) //배틀모드일때 타겟이 null이 아니라면 
            {
                CanMove = false; // 움직임제한
                myanim.SetTrigger("Attack"); // 어택 트리거 활성화
                transform.LookAt(myTarget.transform); // 타겟바라보게 
            }
           


        }



    }
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.CREATE:
                Backbow.SetActive(true);
                Handbow.SetActive(false);
                break;
            case State.IDLE:
                myTarget = null;
                targettingimg.SetActive(false);
                targettingimg = null;
              
                
                myanim.SetTrigger("Idle");
                if(Backbow.activeSelf==false)
                    Backbow.SetActive(true);
                if (Handbow.activeSelf== true)
                    Handbow.SetActive(false);
                break;
            case State.BATTLE:
               
                myanim.SetTrigger("Battle");
                Backbow.SetActive(false);
                Handbow.SetActive(true);
                break;
          
            case State.DIE:
                break;
        }
    }

    protected void StateProcess()
    {
        switch (myState)
        {
            case State.CREATE:
                break;
            case State.IDLE:
                break;
            case State.BATTLE:


                break;
         
            case State.DIE:
                break;
        }
    }

}
    

   

