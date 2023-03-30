using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeProjectileBehaviour : MonoBehaviour
{
    private void Start() 
    {

    }

    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject);
    }

    private void ProjectileDeath() 
    {
        
    }
}
