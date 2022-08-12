using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossDragon : MonoBehaviour
{
    public static BossDragon instance;
    public enum State
    {
        SLEEP, IDLE, BATTLE1, BATTLE2, DIE
    }
    public State myState = State.SLEEP;
    public UnitCode unitCode;
    public Collider[] targetIndistance;
    public GameObject myTarget;
    public LayerMask TARGET;
    Animator myanim;
    public Stat stat;
    public GameObject hpbarcanvas;
    public GameObject FireballPrefab;
    public GameObject FireballPosition;
    public GameObject Dash;
    public GameObject MeteorPrefab;
    public BoxCollider MeteorArea;
    public Image Hpbar;
    public GameObject Metor;
    public GameObject basicattack;
    public AudioClip Dragonbgm;
    public AudioClip beforebgm;
    public AudioClip fly;
    public AudioClip Fireballsound;
    CharacterManger characterManger;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
        stat = new Stat();
        stat = stat.SetUnitStat(unitCode);
        myanim = GetComponent<Animator>();
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        StateProcess();
        characterManger = GameObject.FindWithTag("Player").GetComponent<CharacterManger>();
        Hpbar.fillAmount = (float)stat.curHp / (float)stat.maxHp;
        if (stat.curHp <= 0f)
        {
            StopAllCoroutines();
            ChangeState(State.DIE);
        }

        
    }
    public void Fireball()
    {
        
        Vector3 pos = myTarget.transform.position;
        transform.LookAt(pos);
        SoundManger.instance.SFXPlay("DragonFireBall", Fireballsound);
        Instantiate(FireballPrefab, FireballPosition.transform.position, FireballPosition.transform.rotation, this.transform);


    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HitDragon");
        if (myState != State.DIE)
        {
            switch (collision.gameObject.tag)
            {
                case "Arrow":
                    int playerDamage = Random.Range((int)(characterManger.stat.Damage * 1), (int)(characterManger.stat.Damage * 1.2));
                    stat.curHp -= playerDamage;
                    if (stat.curHp <= 0f)
                    {
                        ChangeState(State.DIE);
                    }
                    break;
                case "Bullet":
                    playerDamage = (int)(characterManger.stat.Damage * 0.5);
                    stat.curHp -= playerDamage;   
                    if (stat.curHp <= 0f)
                    {
                        ChangeState(State.DIE);
                    }
                    break;
            }

        }
    }


    IEnumerator Battle1()
    {

        while (true)
        {
            float distance = Vector3.Distance(transform.position, myTarget.transform.position);
            Debug.Log(distance);


            if (distance > 7f)
            {
                myanim.SetTrigger("FireBall");
                yield return new WaitForSeconds(3f);
            }
            else
            {
                myanim.SetTrigger("BasicAttack");
                basicattack.SetActive(true);
                yield return new WaitForSeconds(3f);
                basicattack.SetActive(false);
            }

        }





    }
    IEnumerator Battle2()
    {

        while (true)
        {
            float distance = Vector3.Distance(transform.position, myTarget.transform.position);
            if (distance > 7f)
            {
                int p = Random.Range(1, 101);
                Debug.Log(p);
                if (p <= 20)
                {
                    //StartCoroutine(CreateMeteor());
                    yield return new WaitForSeconds(12f);
                }
                else
                {
                    myanim.SetTrigger("Fireball");
                    yield return new WaitForSeconds(3f);
                }


            }
            else
            {
                myanim.SetTrigger("BasicAttack");
                basicattack.SetActive(true);
                yield return new WaitForSeconds(3f);
                basicattack.SetActive(false);
            }


            yield return null;
        }

    }

    IEnumerator CreateMeteor()
    {
        myanim.SetBool("Isfly", true);
        myanim.SetTrigger("Fly");
       
        yield return new WaitForSeconds(5.1f);
        transform.LookAt(myTarget.transform.position);
        Instantiate(Metor, myTarget.transform.position, Quaternion.identity, this.transform);
        yield return new WaitForSeconds(3f);
        myanim.SetTrigger("Ground");
        yield return new WaitForSeconds(3f);
        myanim.SetBool("IsFly", false);

    }
    IEnumerator Disapearing()//Disapearing �ڷ�ƾ
    {

        yield return new WaitForSeconds(3.0f); // 1�ʵ�

        Destroy(this.gameObject);// while �� ������ ���� ���� 
    }

    void ChangeState(State s) // State ���� 
    {

        if (myState == s) return; // ���� �� state �� �Է¹��� state �� ���ٸ� return 
        myState = s; // �ƴ϶�� �� state �� �Է¹��� s ���� 

        switch (myState)
        {
            case State.SLEEP:

                break;
            case State.IDLE:
                BGM.instance.ChangeMusic(2);

                break;
            case State.BATTLE1:
                StartCoroutine(Battle1());

                break;
            case State.BATTLE2:

                StartCoroutine(CreateMeteor());
                if (myanim.GetBool("IsFly") == false)
                {
                    StopCoroutine(CreateMeteor());
                    StartCoroutine(Battle2());
                }



                break;
            case State.DIE:
                BGM.instance.ChangeMusic(1);
                myanim.SetBool("Idle", false);
                myanim.SetTrigger("Die");
                hpbarcanvas.SetActive(false);
                StartCoroutine(Disapearing());
                characterManger.meetboos = false;

                break;

        }

    }

    void StateProcess()
    {

        switch (myState)
        {
            case State.SLEEP:
                if (myanim.GetBool("Idle") == true)
                {
                    ChangeState(State.IDLE);
                }
                break;
            case State.IDLE:

                targetIndistance = Physics.OverlapSphere(transform.position, 50f, TARGET);
                myTarget = targetIndistance[0].gameObject;
                if (myTarget != null)
                {
                    ChangeState(State.BATTLE1);
                }
                break;
            case State.BATTLE1:
                if (stat.curHp < stat.maxHp / 2)
                {
                    ChangeState(State.BATTLE2);
                }
                break;
            case State.BATTLE2:
                if(myanim.GetBool("IsFly")==true)
                {
                    SoundManger.instance.SFXPlay("Fly", fly);
                }
                break;
            case State.DIE:
                break;

        }
    }
}