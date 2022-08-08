using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Inst = null;
    Queue<GameObject> mypool = new Queue<GameObject>();

    private void Awake()
    {
        Inst = this;   
    }


    public GameObject GetGameObject(GameObject obj)
    {
        GameObject createObj = null;
        if(mypool.Count>0)
        {
            createObj=mypool.Dequeue();
            createObj.SetActive(true);
            return mypool.Dequeue();
        }

        return Instantiate(obj);
    }

    public void ReleaseGameObject(GameObject obj)
    {
        obj.SetActive(false);
        mypool.Enqueue(obj);
    }
}
