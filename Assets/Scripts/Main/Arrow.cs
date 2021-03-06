using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject arrow;
    public Transform StartArrowPos;
    public int Damage;
    public float speed = 15f;
    public GameObject Target;
   
    // Start is called before the first frame update
    void Start()
    {
      

        Target = GameObject.Find("Archer").GetComponent<CharacterManger>().myTarget;//타겟 = 캐릭터의타겟 
    }

    // Update is called once per frame
    void Update()
    {
      
            
            if(Target!=null)
            {
                transform.position = Vector3.Lerp(transform.position, Target.transform.position,Time.deltaTime * 10f);//타겟의 거리까지 1초만에 날아가게 

            transform.LookAt(Target.transform);
        }
       
        
    }
    private void OnTriggerEnter(Collider other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Monster"))) // 닿은 오브젝트 이름이 Monster일때
        {

            Destroy(this.gameObject); // 화살 삭제
            Debug.Log("Hit"); // 디버그용 
        }
    }
    
  
}
