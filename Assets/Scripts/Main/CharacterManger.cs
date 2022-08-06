using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class CharacterManger : MonoBehaviour
{
    public static CharacterManger instance;
    public enum State
    {
        CREATE, IDLE, BATTLE,DIE 
    }
    public State myState = State.CREATE;
    public Transform cam;
    public Stat stat; // 스탯 받아오기위해 
    public UnitCode unitCode; // 유닛코드 설정을 위해 
 
    [SerializeField]Transform characterBody;
    Animator myanim;
    float distance =15f; //자동공격 감지거리 
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
    public Renderer[] meshs;// 플레이어 mesh 배열 
    public bool isDamage; // 무적시간 재는용 
    public bool fixTarget; //불값을 이용해 타겟고정할예정 
     public GameObject Backbow;
    public GameObject Handbow;
    public GameObject inventory;
    public GameObject MyNickname;
    public Image Myhpbar; //캐릭터 위 hp바
    public Image Mympbar;//캐릭터 위 mp바
    public Image hpbar;//상단 hp바
    public Image mpbar; //상단 mp바
    public Text hp, mp;//상단 hp mp 표시 텍스트
    public List<Quest> myquests = null;
    public int questchapter = 1;
    public bool meetboos = false;
    public GameObject Snipingprefab;
    public Image cooltimeimg;
    public Text cooltimetext;
    bool skill1=true;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        stat = new Stat();// 스탯 뉴 스탯 
        
        
        stat = stat.SetUnitStat(unitCode); // 스탯에 SetUnitStat 호출 (유닛코드로 구별하여) 유니티에서 wolf 로 설정해주면됨 
        Load();
     
        
        myanim = GetComponent<Animator>();
        Myhpbar.fillAmount = (float)stat.curHp / (float)stat.maxHp;
        hpbar.fillAmount = (float)stat.curHp / (float)stat.maxHp;
        Mympbar.fillAmount = (float)stat.curMp / (float)stat.maxMp;
        mpbar.fillAmount = (float)stat.curMp / (float)stat.maxMp;
        hp.text = stat.curHp + "/" + stat.maxHp.ToString();
        mp.text = stat.curMp + "/" + stat.maxMp.ToString();
        CanMove = true;
        
    }
    private void OnApplicationQuit()//게임 종료시 스탯 save
    {
        Save(); 
    }
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(stat); // jdata 에 내 스탯 넣어주기
        File.WriteAllText(Application.dataPath + "/Resources/Mystat.txt", jdata); // 이파일을 Resources 폴더에 Mystat jdata로 저장해준다 

    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/Mystat.txt"); // 내 스탯을 읽어오고 
        stat = JsonConvert.DeserializeObject<Stat>(jdata); // Convert하여 stat에 넣어준다 

    }
    public void myCondition()
    {
        Myhpbar.fillAmount = Mathf.Lerp(Myhpbar.fillAmount, (float)stat.curHp / (float)stat.maxHp, Time.deltaTime * 10f);//내 hp바 계산
        hpbar.fillAmount = Mathf.Lerp(Myhpbar.fillAmount, (float)stat.curHp / (float)stat.maxHp, Time.deltaTime * 10f);//상단 hp바 계산
        Mympbar.fillAmount = Mathf.Lerp(Mympbar.fillAmount, (float)stat.curMp / (float)stat.maxMp, Time.deltaTime * 10f);//내 mp바 계산
        mpbar.fillAmount = Mathf.Lerp(Mympbar.fillAmount, (float)stat.curMp / (float)stat.maxMp, Time.deltaTime * 10f);//상단 mp바 계산
        hp.text = stat.curHp + "/" + stat.maxHp.ToString();//상단 hp text표현
        mp.text = stat.curMp + "/" + stat.maxMp.ToString();//상단 mp text표현 
    }
    private void Update()
    {
        myCondition();
        MyNickname.transform.position = Camera.main.WorldToScreenPoint(this.transform.position+new Vector3(0,2.7f,0));
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (myState==State.BATTLE)
        {
            if(fixTarget==false&&Targets!=null)// 스타트코루틴
            {
                StartCoroutine(SetTartget());
            }
           else // 스탑코루틴 
            {
                StopCoroutine(SetTartget());
            }
        }
        
        if (myanim.GetBool("IsAttack") == false && CanMove == true && inventory.activeSelf==false)
        {
            move(h, v);
        
        }
        if(inventory.activeSelf==true)
        {
            
            myanim.SetBool("IsMoving", false);
            StopAllCoroutines();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {

        
        if(other.tag == "EnemyAttack")
        {
            if (!isDamage) // 무적시간 이 끝났을때 
            {



                 
                        int dmage;
                        dmage = other.GetComponentInParent<Monster>().stat.Damage; // 몬스터 데미지 얻어와서 hp - 해줄려고
                        stat.curHp -= dmage;
                

                StartCoroutine(Ondamage()); // 코루틴시작 

            }
        }
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BossDragon"))
        {
            int dmage;
            dmage = collision.gameObject.GetComponentInParent<BossDragon>().stat.Damage;    
            stat.curHp -= dmage;
        }
    }


    IEnumerator Ondamage()
    {
        if(stat.curHp<=0)// hp가 0보다 적다면 
        {
            myanim.SetTrigger("Die"); // 다이 애니메이션 실행 

        }
        else if(stat.curHp>0)
        {
            isDamage = true; // 무적시간 재는용 true 라면 무적 

            for (int i = 0; i < meshs.Length; i++) // mesh 가 여러개있어서 배열로 받아놓고 for 문돌려준다 
            {
                meshs[i].material.color = Color.red; //모든 mseh 를 빨간색으로변경 
            }

            yield return new WaitForSeconds(0.3f); // 0.3초후 
            for (int i = 0; i < meshs.Length; i++)
            {
                meshs[i].material.color = Color.white; // 모든 mesh 흰색으로 변경
            }
            isDamage = false; // 무적시간 재는용 false 

        }
       
    }


    void move(float h, float v)
    {
        Vector2 Move = new Vector2(h, v);
        if (myanim.GetBool("EqipBow") != true)
        {
            bool IsMoving = Move.magnitude != 0;
            myanim.SetBool("IsMoving", IsMoving);
            if (IsMoving)
            {
                Vector3 LookForwad = new Vector3(cam.forward.x, 0f, cam.forward.z).normalized;
                Vector3 lookRight = new Vector3(cam.right.x, 0f, cam.right.z).normalized;
                Vector3 moveDir = LookForwad * Move.y + lookRight * Move.x;

                characterBody.forward = moveDir;
                transform.position += moveDir * (Time.deltaTime * stat.moveSpeed);
            }

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
        
            if (Targets.Count != 0)//판별된 적이 1명이라도 있다면 실행되게 
            {

                if (targettingimg != null) //새로 타겟팅 됬을때 이미지를 초기화시켜주기위해 
                    targettingimg.SetActive(false);

                myTarget = Targets[0];//첫번째 타겟을 나의타겟으로 넣어놓고 
                curtarget = Vector3.Distance(transform.position, Targets[0].transform.position);// 그 타겟과 나의 거리를 계산한뒤 

                for (int i = 1; i < Targets.Count; i++) //for 문을 돌려주는데 i 의 0번째는 이미 들어갔기때문에 1번째부터 비교하기위해 i= 1 
                {

                    float dist = Vector3.Distance(transform.position, Targets[i].transform.position);  //[i]번째 타겟과 내 거리를 계산하고
                    if (dist < curtarget)// [i]번째 타겟의 거리가 현재타겟의 거리보다 작으면 실행
                    {
                        myTarget = Targets[i]; //나의타겟은 [i]번째 타겟으로바뀌고
                        curtarget = dist; // 현재 타겟과의거리도 [i]번째 타겟의거리로 넣어준다.

                    }

                }
                if(meetboos==false)
            {
                targettingimg = myTarget.transform.Find("Canvas").transform.gameObject;// 나의 타겟에 캔버스를 찾아서 
                targettingimg.SetActive(true);// 캔버스를 보이게해준다.
            }
               
            }
            else if (Targets.Count == 0 && targettingimg != null)//만약 타겟이 없고 타겟팅이미지가 null이아니면 안보여지게하기위해 
            {
                targettingimg.SetActive(false);
                myTarget = null;
            }
        if (meetboos == false)
        {
            if (myTarget != null)
                if (myTarget.GetComponent<Monster>().myState == Monster.State.DIE)
                {
                    targettingimg.SetActive(false);
                    myTarget = null;
                }
        }
           
        
        
      yield return null;    
    }



   

    public void DrawArrow()
    {
        GameObject go = Instantiate(arrow, arrowpoint.transform.position, arrowpoint.transform.rotation);

    }
    public void ArrowReocil()
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
                if(myTarget !=null) // 내타겟이 null 이 아니라면 
                {
                    float dist = Vector3.Distance(this.transform.position,myTarget.transform.position);  // 거리값을구해서
                    myanim.SetTrigger("Attack"); // 어택 트리거 활성화 
                    transform.LookAt(myTarget.transform); // 타겟바라보게 



                }
               
            }
           


        }



    }
    IEnumerator Sniping()
    {
        Instantiate(Snipingprefab,myTarget.transform.position,Quaternion.identity);
        
        CanMove = false;
        yield return new WaitForSeconds(4f);
        myanim.SetTrigger("EndSniping");
        Destroy(Snipingprefab);
       
        CanMove = true;

    }
    IEnumerator CoolTime(float cooltime)
    {


        skill1 = false;
        cooltimetext.gameObject.SetActive(true);
        cooltimeimg.gameObject.SetActive(true);
        cooltimetext.text = cooltime.ToString();
        float fillimg = cooltime;
        while (cooltime>0.0f)
        {
            cooltime -=Time.deltaTime;
            cooltimeimg.fillAmount = cooltime/fillimg;
            
           
            cooltimetext.text = cooltime.ToString("0.0");
            yield return new WaitForFixedUpdate();
        }
        cooltimeimg.gameObject.SetActive(false);
        cooltimeimg.fillAmount = 1;
        cooltimetext.gameObject.SetActive(false);
        skill1 = true;
        yield break;
    }
    public void Skillbutton1()
    {

       

        if (myanim.GetBool("IsAttack")==false&& skill1 == true)
        {
            if(myTarget!=null&&stat.curMp>=30)
            {
                stat.curMp -= 30;
                myanim.SetTrigger("Sniping");
                myanim.SetBool("IsMoving", false);
                transform.LookAt(myTarget.transform);
                StartCoroutine(CoolTime(10f));
                StartCoroutine(Sniping());
              
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
    

   

