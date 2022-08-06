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
    private void Update()
    {
        StateProcess();
        Hpbar.fillAmount = (float)stat.curHp / (float)stat.maxHp;
        if (stat.curHp <= 0f)
        {
            StopAllCoroutines();
            ChangeState(State.DIE);
        }

    }
    public void Fireball()
    {
        Debug.Log("FireBall");
        Vector3 pos = myTarget.transform.position;
        transform.LookAt(pos);
        Instantiate(FireballPrefab, FireballPosition.transform.position, FireballPosition.transform.rotation, this.transform);


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (myState != State.DIE)
        {
            switch (collision.gameObject.tag)
            {
                case "Arrow":
                    int playerDamage = Random.Range((int)(CharacterManger.instance.stat.Damage * 1), (int)(CharacterManger.instance.stat.Damage * 1.2));
                    stat.curHp -= playerDamage;
                    if (stat.curHp <= 0f)
                    {
                        ChangeState(State.DIE);
                    }
                    break;
                case "Bullet":
                    playerDamage = (int)(CharacterManger.instance.stat.Damage * 0.7);
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
                Debug.Log("BasicAttack");
                myanim.SetTrigger("BasicAttack");
                yield return new WaitForSeconds(2f);
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
                    StartCoroutine(CreateMeteor());
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
                yield return new WaitForSeconds(2f);
            }


            yield return null;
        }

    }

    IEnumerator CreateMeteor()
    {
        myanim.SetTrigger("Fly");
        myanim.SetBool("Isfly", true);
        yield return new WaitForSeconds(5.1f);
        transform.LookAt(myTarget.transform.position);
        Instantiate(Metor, myTarget.transform.position, Quaternion.identity, this.transform);
        yield return new WaitForSeconds(3f);
        myanim.SetTrigger("Ground");
        yield return new WaitForSeconds(3f);
        myanim.SetBool("IsFly", false);

    }
    IEnumerator Disapearing()//Disapearing 코루틴
    {

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

        switch (myState)
        {
            case State.SLEEP:

                break;
            case State.IDLE:

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

                myanim.SetTrigger("Die");
                hpbarcanvas.SetActive(false);
                StartCoroutine(Disapearing());
                CharacterManger.instance.meetboos = false;

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
                break;
            case State.DIE:
                break;

        }
    }
}
