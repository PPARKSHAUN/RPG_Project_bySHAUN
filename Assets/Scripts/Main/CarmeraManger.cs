using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmeraManger : MonoBehaviour
{
    GameObject player;
    public float x = 0f;
    public float y = 4f;
    public float z = -5f;
    Vector2 m_Input;
   
    Vector3 Armposition;
    public Transform follow;
    

    private void Awake()
    {
       
       
    }

    private void LateUpdate()
    {
        player = GameObject.FindWithTag("Player");

        Armposition.y = player.transform.position.y + 1.6f;
        Armposition.x = player.transform.position.x ;
        Armposition.z = player.transform.position.z ;
        follow.position = Armposition;

        turn();
    }

    void turn()
    {
        if (Input.GetMouseButton(1))
        {
            m_Input.x = Input.GetAxis("Mouse X");
            m_Input.y = Input.GetAxis("Mouse Y");

            if (m_Input.magnitude != 0)
            {
                Quaternion q = follow.rotation;
                q.eulerAngles = new Vector3(0, q.eulerAngles.y + m_Input.x * 5f, q.eulerAngles.z);
                follow.rotation = q;

            }
        }
    }
}
