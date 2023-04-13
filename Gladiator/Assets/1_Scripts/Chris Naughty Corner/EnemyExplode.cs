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

    private void Breaking()
    {
        //_rb.useGravity = false;
        GameObject frac = Instantiate(fractured, transform.position, transform.rotation);
        
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
