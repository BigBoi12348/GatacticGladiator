using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBladeProjectileBehaviour : MonoBehaviour
{
    private void Start() 
    {
        Invoke("ProjectileDeath", 8f);
    }   

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<EnemyBodyPart>(out EnemyBodyPart enemyBodyPart))
        {
            enemyBodyPart.DestroyMe();
            ProjectileDeath(); 
        }
    }

    // private void OnTriggerEnter(Colliod other) 
    // {
        
    // }

    private void ProjectileDeath() 
    {
        Destroy(gameObject);
    }
}
