using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManger : MonoBehaviour
{
    public enum State
    {
        CREATE, IDLE, BATTLE,DIE 
    }
    public State myState = State.CREATE;
    public Transform cam;
    public GameObject Backbow;
    public GameObject Handbow;
    float moveSpeed = 6f;
    float rotationSpeed = 5f;
    Rigidbody body;
    Animator myanim;
    Vector3 movement;

     void Awake()
    {
        myanim = GetComponent<Animator>();  
        body = GetComponent<Rigidbody>();
       
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");



        move(h,v);
        turn(h, v);
        if(Input.GetMouseButtonUp(0))
        {
            ChangeState(State.BATTLE);
        }

        if (Input.GetMouseButton(1))
        {
            ChangeState(State.IDLE);
        }
    }
    void move(float h, float v)
    {
        Debug.DrawRay(cam.position, cam.forward, Color.red);
        
        movement.Set(h, 0, v);
       
       if(h ==0 && v == 0 )
        {
            myanim.SetBool("IsMoving", false);
        }
       else if( myanim.GetBool("EqipBow") != true)
        {
            myanim.SetBool("IsMoving", true);
        }
        if (myanim.GetBool("EqipBow") != true)
        {
          
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            body.MovePosition(transform.position + movement);
        }
            
    }

    void turn(float h, float v)
    {
        if (h == 0 && v == 0 )
            return;
        else if(myanim.GetBool("EqipBow") != true)
        {
            Quaternion Rotation = Quaternion.LookRotation(movement);
            body.rotation = Quaternion.Slerp(body.rotation, Rotation, rotationSpeed * Time.deltaTime);
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
                myanim.SetTrigger("Idle");
                if(Backbow.activeSelf==false)
                    Backbow.SetActive(true);
                if (Handbow.activeSelf== true)
                    Handbow.SetActive(false);
                break;
            case State.BATTLE:
                moveSpeed = 3f;
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
    

   

