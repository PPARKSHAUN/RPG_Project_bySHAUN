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
   
    void Awake()
    {
        myanim = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        CanMove = true;
      
    }

    private void Update()
    {
        targetIndistance = Physics.OverlapSphere(transform.position, distance, targetmask);
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (myState==State.BATTLE)
        {
            SetTartget();
        }
        if (targetIndistance == null)
        {
            ChangeState(State.IDLE);
        }
        if (myanim.GetBool("IsAttack") == false && CanMove == true)
        {
            move(h, v);
            turn(h, v);
        }

    }
    void FixedUpdate()
    {
        

       
        
      
       
        
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
 
    void SetTartget()
    {
      
        Targets.Clear(); //�迭�� ��ӽ��̴°� �����Ϸ��� Ŭ���� �ϰ� �ޱ� 
       targetIndistance = Physics.OverlapSphere(transform.position, distance, targetmask); // ����ġ ����, distance=��Ÿ� ��ü�ݰ�,target����ũã�����Ѱ�

        for (int i = 0;i<targetIndistance.Length; i++) //����Ÿ��ȿ� ���͸�ŭ �����ַ��� �Ѱ� 
        {
            Transform target = targetIndistance[i].transform; //������ġ 
            Vector3 dirTarget = (target.position-transform.position).normalized; //������ġ - �������� normalized
            if(Vector3.Angle(transform.forward,dirTarget)<angleRange/2) // ������ġ�� ���� ���س��� ��ä�þȿ� �����ϴ��� 
            {
                float dstTarget=Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position,dirTarget,distance,etcmask))
                {
                    Targets.Add(target.transform.gameObject);
                 

                }
            }
        }
        if(Targets.Count != 0)//�Ǻ��� ���� 1���̶� �ִٸ� ����ǰ� 
        {

            if (targettingimg != null) //���� Ÿ���� ������ �̹����� �ʱ�ȭ�����ֱ����� 
                targettingimg.SetActive(false);

            myTarget = Targets[0];//ù��° Ÿ���� ����Ÿ������ �־���� 
            curtarget = Vector3.Distance(transform.position, Targets[0].transform.position);// �� Ÿ�ٰ� ���� �Ÿ��� ����ѵ� 

            for(int i=1;i<Targets.Count;i++) //for ���� �����ִµ� i �� 0��°�� �̹� ���⶧���� 1��°���� ���ϱ����� i= 1 
            {
                float dist=Vector3.Distance(transform.position,Targets[i].transform.position);  //[i]��° Ÿ�ٰ� �� �Ÿ��� ����ϰ�
                if(dist<curtarget)// [i]��° Ÿ���� �Ÿ��� ����Ÿ���� �Ÿ����� ������ ����
                {
                    myTarget=Targets[i]; //����Ÿ���� [i]��° Ÿ�����ιٲ��
                    curtarget = dist; // ���� Ÿ�ٰ��ǰŸ��� [i]��° Ÿ���ǰŸ��� �־��ش�.
                }
            }

            targettingimg = myTarget.transform.Find("Canvas").transform.gameObject;// ���� Ÿ�ٿ� ĵ������ ã�Ƽ� 
            targettingimg.SetActive(true);// ĵ������ ���̰����ش�.
        }
        else if(Targets.Count==0 && targettingimg != null)//���� Ÿ���� ���� Ÿ�����̹����� null�̾ƴϸ� �Ⱥ��������ϱ����� 
        {
            targettingimg.SetActive(false);
        }

        targetTomyPosition=Vector3.Distance(transform.position,myTarget.transform.position);
        if(targetTomyPosition>distance)
        {
            myTarget = null;
            targettingimg.SetActive(false);
            targettingimg = null;
            curtarget = 0f;
        }

      
    }
            
    
   /* private void OnDrawGizmos()
    {
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);

    }*/

    public void DrawArrow()
    {
        GameObject go = Instantiate(arrow, arrowpoint.transform.position, arrowpoint.transform.rotation);

    }
    public void ArrowRecoil()
    {
        
    }
    public void OnclickAttackButton()
    {
        if(myanim.GetBool("IsAttack")==false)//�Ķ���� IsAttack false �϶� ����
        {
            if (targetIndistance != null) // ùŬ���� ������ ���� �ִٸ� 
            {
                ChangeState(State.BATTLE); //��Ʋ���� �ٲ��ְ� 

            }
            if (myTarget != null) //��Ʋ����϶� Ÿ���� null�� �ƴ϶�� 
            {
                CanMove = false; // ����������
                myanim.SetTrigger("Attack"); // ���� Ʈ���� Ȱ��ȭ
                transform.LookAt(myTarget.transform); // Ÿ�ٹٶ󺸰� 
            }
            else if(myTarget ==null)
            {
                targetIndistance = Physics.OverlapSphere(transform.position, 100f, targetmask);
                for(int i=0;i<targetIndistance.Length;i++)
                {
                    curtarget = Vector3.Distance(transform.position, targetIndistance[0].transform.position);
                    float dist = Vector3.Distance(transform.position, targetIndistance[i].transform.position);
                    if(dist<curtarget)
                    {
                        myTarget = targetIndistance[i].gameObject;
                        curtarget = dist;
                    }

                }

                //���⿡ ���� ����� Ÿ���� �������Ÿ��ö����� �����̰��ϴ��ڵ� 
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
    

   
