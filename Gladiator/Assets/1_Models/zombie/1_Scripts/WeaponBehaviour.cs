using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("This working");
        if(other.gameObject.TryGetComponent<EnemyBodyPart>(out EnemyBodyPart enemyBodyPart))
        {
            enemyBodyPart.DestroyMe();
        }    
    }
}
