using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    /*public GameObject rangeObject;
    BoxCollider rangeCollider;
    public GameObject[] monstercount;
    
    public GameObject monster;

    private void Awake()
    {
        StartCoroutine(spawnmonster());
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
    IEnumerator spawnmonster()
    {
        while(monstercount.Length<10)
        {
            monstercount = GameObject.FindGameObjectsWithTag("Wolf");
            yield return null;

            GameObject Monster = Instantiate(monster, Return_RandomPosition(), Quaternion.identity);
        }
    }*/
}
