using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeProjectileBehaviour : MonoBehaviour
{
    private int _amountOfHitsICanDo;
    public void Init(int hitAmount, bool amIHoming)
    {
        _amountOfHitsICanDo = hitAmount;
        // if(amIHoming)
        // {
        //     StartCoroutine(HomingToEnemy());
        // }
        Invoke("ProjectileDeath", 8f);
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.gameObject.name);
        if(_amountOfHitsICanDo <= 0)
        {
            _amountOfHitsICanDo -= 1;
        }
        else
        {
            ProjectileDeath();
        }
    }
    

    // private IEnumerator HomingToEnemy()
    // {
    //     yield return new WaitForSeconds(0);
    // }

    private void OnCollisionEnter(Collision other) 
    {
        ProjectileDeath();         
    }

    private void ProjectileDeath() 
    {
        Destroy(gameObject);
    }
}
