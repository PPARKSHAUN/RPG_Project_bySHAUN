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

    void Awake()
    {
        myanim = GetComponent<Animator>();  
        body = GetComponent<Rigidbody>();
       
    }
    private void Update()
    {
        Delay(1f);
        SetTartget();
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");



        move(h,v);
        turn(h, v);
        if(Input.GetMouseButtonUp(0))
        {
            ChangeState(State.BATTLE);
        }

        if (Input.GetMouseButton(1))
        {
            ChangeState(State.IDLE);
        }
    }
    void move(float h, float v)
    {
        Debug.DrawRay(cam.position, cam.forward, Color.red);
        
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
    IEnumerator Delay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);//기다림 

        }
    }
    void SetTartget()
    {
        Targets.Clear(); //배열에 계속쌓이는걸 방지하려고 클리어 하고 받기 
       targetIndistance = Physics.OverlapSphere(transform.position, distance, targetmask); // 내위치 중점, distance=사거리 구체반경,target마스크찾기위한거

        for (int i = 0;i<targetIndistance.Length; i++) //내사거리안에 몬스터만큼 돌려주려고 한거 
        {
            Transform target = targetIndistance[i].transform; //몬스터위치 
            Vector3 dirTarget = (target.position-transform.position).normalized; //몬스터위치 - 내포지션 normalized
            if(Vector3.Angle(transform.forward,dirTarget)<angleRange/2) // 몬스터위치가 내가 정해놓은 부채꼴안에 내적하는지 
            {
                float dstTarget=Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position,dirTarget,distance,etcmask))
                {
                    Targets.Add(target.transform.gameObject);
                 

                }
            }
        }

    }
            
    private void OnDrawGizmos()
    {
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);

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
                moveSpeed = 6f;
                myanim.SetTrigger("Idle");
                if(Backbow.activeSelf==false)
                    Backbow.SetActive(true);
                if (Handbow.activeSelf== true)
                    Handbow.SetActive(false);
                break;
            case State.BATTLE:
                moveSpeed = 3f;
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
    

   

