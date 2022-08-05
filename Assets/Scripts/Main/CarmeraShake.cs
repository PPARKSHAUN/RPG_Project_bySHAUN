using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmeraShake : MonoBehaviour
{
    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;
    Quaternion m_originRot;
    public Animator rockanim;
    public GameObject maincam;
    void Start()
    {
        m_originRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
       if (rockanim.GetBool("IsMove") == true)
        {
            
            maincam.SetActive(false);
            Shake();
        }
       else if(rockanim.GetBool("IsMove")==false)
        {
            this.gameObject.SetActive(false);
            maincam.SetActive(true);
        }
       
          
       
    }

    public void Shake()
    {

        Vector3 t_originEuler = transform.eulerAngles;


        float rotX = Random.Range(-m_offset.x, m_offset.x);
        float rotY = Random.Range(-m_offset.y, m_offset.y);
        float rotZ = Random.Range(-m_offset.z, m_offset.z);

        Vector3 t_randomRot = t_originEuler + new Vector3(rotX, rotY, rotZ);
        Quaternion t_rot = Quaternion.Euler(t_randomRot);



        transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
    }
    
}
