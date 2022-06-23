using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmeraManger : MonoBehaviour
{
    GameObject player;
    public float x = 0f;
    public float y = 4f;
    public float z = -5f;
 
    Vector3 cameraPosition;
    Vector3 Armposition;
    [SerializeField] private Transform Rotcamera;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    private void LateUpdate()
    {
       
        cameraPosition.x = player.transform.position.x + x;
        cameraPosition.y = player.transform.position.y + y;
        cameraPosition.z = player.transform.position.z + z;
        Armposition.y = player.transform.position.y + 1.6f;
        Armposition.x = player.transform.position.x ;
        Armposition.z = player.transform.position.z ;
        Rotcamera.position = Armposition;
        this.transform.position = cameraPosition;
        turn();
    }

    void turn()
    {
        if(Input.GetMouseButton(1))
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),0);
            Vector3 camAngle = Rotcamera.rotation.eulerAngles;
   
         
            Rotcamera.rotation = Quaternion.Euler(0, camAngle.y + mouseDelta.x, 0);
        }
       

      
    }

    
}
