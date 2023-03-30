using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeProjectileBehaviour : MonoBehaviour
{
    private void Start() 
    {
        Invoke("ProjectileDeath", 8f);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name);
            ProjectileDeath(); 
        }
    }

    private void ProjectileDeath() 
    {
        Destroy(gameObject);
    }
}
