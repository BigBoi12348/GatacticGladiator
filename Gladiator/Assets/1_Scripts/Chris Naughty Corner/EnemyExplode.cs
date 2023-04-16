using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    public GameObject fractured;
    public float breakforce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon"))
        {
            Breaking();
        }
    }

    // private void OnCollisionEnter(Collision other) 
    // {
    //     Debug.Log(other.gameObject.tag);
    //     if(other.gameObject.CompareTag("PlayerWeapon"))
    //     {
    //         Breaking();
    //     }
    // }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Breaking();
        }
    }


    private void Breaking()
    {
        //_rb.useGravity = false;
        GameObject frac = Instantiate(fractured, transform.position, Quaternion.identity);
        
        foreach (Transform child in frac.transform)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                //rb.useGravity = false;
                Vector3 force = (rb.transform.position - transform.position).normalized * breakforce;
                rb.AddForce(force);
            }
        }
        Destroy(gameObject);
    }
}
