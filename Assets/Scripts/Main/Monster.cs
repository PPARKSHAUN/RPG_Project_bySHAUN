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
    public float speed = 3f;
    public State myState = State.CREATE;
    public int maxHp;
    public int curHp;
    public int Damage;
    public Renderer myrenderer;
    public Animator myanim;
    public LayerMask targetmask;
    public Collider[] targetIndistance;
    public GameObject myTarget;
    public float Attackrange = 3f;
    NavMeshAgent nav;
    public bool roaming = true;
    [SerializeField] Vector3 pos;
    public GameObject AttackPoint;

    private void Awake()
    {
        ChangeState(State.IDLE);
        nav = GetComponent<NavMeshAgent>();

       
    }
    private void OnTriggerEnter(Collider other)//Ʈ���ſ��Ͱ� �ߵ��ɶ� 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Arrow")))//������ Ʈ���ſ� ���̾� �̸��� Arrow �϶� 
        {
           
            Arrow arrow = other.GetComponent<Arrow>(); // Arrow �� GetComponet �ϰ� 
            curHp -= arrow.Damage;// ���� hp ���� arrow �������� ���ְ� 
            StartCoroutine(OnDamage()); // ondamage �ڷ�ƾ ���� 
            ChangeState(State.BATTLE); // �°ԵǸ� Battle ���·� ���� 
            myanim.SetTrigger("Damage");// �°ԵǸ� �� �ִϸ��̼� ������ ���� 
        }
        


    }
    IEnumerator OnDamage() // ondamage�ڷ�ƾ 
    {
        targetIndistance = Physics.OverlapSphere(transform.position, 20f, targetmask); // 20f �ȿ� player Ÿ�ٸ���ũ �ݶ��̴� �ޱ� 
        myTarget = targetIndistance[0].gameObject; // player �� �ϳ��ۿ����⶧���� 0 ���� ��Ÿ�����μ��� 
        if (myTarget != null) // ��Ÿ���� null �� �ƴϸ� 
        {
            ChangeState(State.BATTLE); // ��Ʋ���� �Ѿ�� 
            
        }
        myrenderer.material.color= Color.red; // ���� ���� �������� ���� 
        yield return new WaitForSeconds(0.2f); // 0.2 �� ��ٸ��� 
        if(curHp>0)// ���� hp �� 0����ũ�ٸ� 
        {
            myrenderer.material.color = new Color(1, 1, 1); //��������� 
            CharacterManger characterManger = GameObject.Find("Archer").GetComponent<CharacterManger>();
            characterManger.fixTarget = true; //���� �¾Ҵµ� ���� hp �� 0����ũ��  Ÿ�� ������ ���� ��ž �ڷ�ƾ ���� bool ���� 
        }
        if (curHp <= 0)//���� hp �� 0���� �۴ٸ�
        {
            ChangeState(State.DIE);
        }

    }
    IEnumerator Disapearing()//Disapearing �ڷ�ƾ
    {
        
        myrenderer.material.color = Color.grey;// ���� ���� ȸ������ ����
        yield return new WaitForSeconds(1.0f); // 1�ʵ�
       
        float dist = 1.0f; //1.0f ��ŭ ���������Ϸ��� dist ����
        while (dist > 0.0f)// dist �� 0���� �۾��������� ���ѷ���
        {
            float delta = Time.deltaTime * 0.5f; 
            this.transform.Translate(-Vector3.up * delta); // �ʴ� 0.5f �� �������� 
            dist -= delta; // dist ���� ���ش� delta ��ŭ 
            yield return null;
        }
        Destroy(this.gameObject);// while �� ������ ���� ���� 
    }

  
    IEnumerator Battle()
    {
     
        myanim.SetBool("IsRunnig", true); // �پ���� �ִϸ��̼� ��������� 
        while (true)
        {
            
            myanim.SetBool("IsWalking", false); // walking �� true �� �ȹٲ������ 

            nav.SetDestination(myTarget.transform.position); // ���󰡰� �ϱ����� �ڵ� 
            if (myanim.GetBool("IsAttack")==true || myanim.GetBool("IsDamage") ==true ) // ���ݾִϸ��̼� �̳� �������ִϸ��̼� �����ϳ��� ������̶�� �������� ���߰�
            {
                nav.isStopped=true;
            }
            else // �ƴ϶�� ��������
            {
                nav.isStopped = false;
            }
           

            float distance = Vector3.Distance(transform.position, myTarget.transform.position); // �÷��̾�� ���ͻ��� �Ÿ� �������
            if (distance <= Attackrange) // �Ÿ��� ���û�Ÿ� ���� ª�ų� ���ٸ� 
            {
                if (myanim.GetBool("IsAttack") == false) // �������̾ƴ϶�� 
                {
                    myanim.SetTrigger("Attack"); //���ݾִϸ��̼� ��� 
                    StartCoroutine(Attack()); // �ڷ�ƾ���� 
                }


            }






            yield return null;
        }
    }

  
    IEnumerator Attack() 
    {
        AttackPoint.SetActive(true); // ���� �ݸ��� Ȱ��ȭ 
        

        yield return new WaitForSeconds(1f);// 1���� 
        AttackPoint.SetActive(false);// �����ݸ��� ��Ȱ��ȭ 


}
IEnumerator Roaming()
    {
        
       
            pos = new Vector3(); //������ ����
            pos.x = Random.Range(-3f, 3f); //������ x ���� -3~3 ���� ������
            pos.y = 0.116f;
            pos.z = Random.Range(-3f, 3f); // ������ z ���� -3~3 ���� ������
          
      
       

        myanim.SetBool("IsWalking", true); // ó���� �����̴ϱ� Ʈ�� 
        while (roaming) 
        {
           

            var dir = (pos - this.transform.position).normalized; // pos ������ - ���������� normalized ���� 
            this.transform.LookAt(pos); // ���Ͱ� �̵��Ҷ� �̵��ϴ°� �ٶ󺸰��ϱ����� 
            this.transform.position += dir * speed * Time.deltaTime; // ���������ǿ� normalize * ������ speed * Time.deltaTime �����ֱ� 
            float distance = Vector3.Distance(transform.position, pos); // ���Ϳ� ������ ���� �Ÿ� ���ϱ� 
            if (distance <=0.1f) // 0.1 ���϶�� 
            {
                myanim.SetBool("IsWalking", false); // �ȴ� ��� false �� Idle ������ 
                yield return new WaitForSeconds(Random.Range(1f, 3f)); // �������� 1~3 �ʱ�ٸ��� 
                myanim.SetBool("IsWalking", true); // �ٽ� �Ȱ��ϰ� 
                pos.x = Random.Range(-3f, 3f);// ��ġ����
                pos.z = Random.Range(-3f, 3f);//��ġ���� 
               

            }
            
            yield return null;  
        }

       
       
    

    }
    void ChangeState(State s) // State ���� 
    {

        if (myState == s) return; // ���� �� state �� �Է¹��� state �� ���ٸ� return 
        myState = s; // �ƴ϶�� �� state �� �Է¹��� s ���� 

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
                speed = 3.5f; // ���� ���ǵ� 3.5 ������ 
                roaming = false; // while �� �ȵ��� ���� 
               StopCoroutine(Roaming()); // ��ž�ڷ�ƾ �ι� 
                
                StartCoroutine(Battle()); // ��ŸƮ �ڷ�ƾ ��Ʋ 
                break;
            case State.DIE: // die ���·ε����ϸ� 
               
                myanim.SetTrigger("Dead"); // Dead Ʈ���� �۵����� Dead �ִϸ��̼� ������ 
                StartCoroutine(Disapearing()); // Disapearing �ڷ�ƾ ����
                CharacterManger characterManger = GameObject.Find("Archer").GetComponent<CharacterManger>();
                characterManger.fixTarget = false;// ���� �¾Ҵµ� ���� hp �� 0�����۰ų� ���ٸ� Ÿ�� ������ ���� ��ŸƮ �ڷ�ƾ ���� bool ���� 
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