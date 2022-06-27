using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargettingImg : MonoBehaviour
{
    public GameObject targetimg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetimg.transform.position=Camera.main.WorldToScreenPoint(transform.position);
    }
}
