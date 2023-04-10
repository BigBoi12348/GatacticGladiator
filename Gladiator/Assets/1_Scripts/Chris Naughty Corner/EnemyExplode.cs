using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    public GameObject fractured;
    public float breakforce;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name.Equals("Collider"))
    //    {
    //        Debug.Log("ronan is hay");
    //        breakthing();
    //    }
    //}

   
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            breakthing();
        }
    }
    public void breakthing()
    {
        GameObject frac = Instantiate(fractured, transform.position, transform.rotation);
        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakforce;
            rb.AddForce(force);
        }
        Destroy(gameObject);
    }
    
}
