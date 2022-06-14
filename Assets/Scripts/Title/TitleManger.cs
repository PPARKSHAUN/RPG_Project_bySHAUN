using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManger : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject intropanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime(4f));
    }

    IEnumerator DelayTime(float time)
    {

        yield return new WaitForSeconds(time);
        intropanel.SetActive(false);
        menupanel.SetActive(true);
    }
}
