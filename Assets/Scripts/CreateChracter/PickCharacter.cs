using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.DrawRay(transform.position, transform.forward * 999.0f, Color.red, 0.3f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, int.MaxValue,1<<LayerMask.NameToLayer("Warrior")))
            {
                if(hit.collider != null)
                {
                    hit.transform.GetComponent<Animator>().SetTrigger("Pick");
                }
               
               

            }
        }


    }




}

