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

        Target = GameObject.FindWithTag("Player").GetComponent<CharacterManger>().myTarget;//타겟 = 캐릭터의타겟 
    }

    // Update is called once per frame
    void Update()
    {

      

        if (Target!=null)
        {
          
            transform.position = Vector3.Lerp(transform.position, Target.transform.position,Time.deltaTime * 10f);//타겟의 거리까지 1초만에 날아가게 

            transform.LookAt(Target.transform);
        }
       
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.Equals(LayerMask.NameToLayer("Monster")))
        {
           
            Destroy(this.gameObject); // 화살 삭제
        }
    }
  
    
  
}
