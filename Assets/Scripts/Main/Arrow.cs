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
      

        Target = GameObject.Find("Archer").GetComponent<CharacterManger>().myTarget;//Ÿ�� = ĳ������Ÿ�� 
    }

    // Update is called once per frame
    void Update()
    {
      
            
            if(Target!=null)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 1f);//Ÿ���� �Ÿ����� 1�ʸ��� ���ư��� 

            }
       
        
    }
   
}