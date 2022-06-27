using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject arrow;
    public Transform StartArrowPos;
 
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
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 1f);//타겟의 거리까지 1초만에 날아가게 

            }
       
        
    }
   
}
