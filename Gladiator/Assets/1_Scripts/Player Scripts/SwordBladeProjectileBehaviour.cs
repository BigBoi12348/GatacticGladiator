using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeProjectileBehaviour : MonoBehaviour
{
    private void Start() 
    {
        Invoke("ProjectileDeath", 8f);
    }   

    // private void OnTriggerEnter(Collider other) 
    // {
    //     ProjectileDeath();         
    // }

    private void ProjectileDeath() 
    {
        Destroy(gameObject);
    }
}
