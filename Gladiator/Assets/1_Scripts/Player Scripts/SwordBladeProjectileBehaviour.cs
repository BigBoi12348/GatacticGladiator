using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeProjectileBehaviour : MonoBehaviour
{
    private int _amountOfHitsICanDo;
    public void Init(int hitAmount)
    {
        _amountOfHitsICanDo = hitAmount;
        Invoke("ProjectileDeath", 8f);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.name);
        if(_amountOfHitsICanDo <= 0)
        {
            _amountOfHitsICanDo -= 1;
        }
        else
        {
            ProjectileDeath();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        ProjectileDeath();         
    }

    private void ProjectileDeath() 
    {
        Destroy(gameObject);
    }
}
