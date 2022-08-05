using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    // Update is called once per frame
    void Update()
    {
        FllowTaget();

    }
    public void FllowTaget()
    {
        Vector3 target =Vector3.forward;
        this.transform.position=target;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
