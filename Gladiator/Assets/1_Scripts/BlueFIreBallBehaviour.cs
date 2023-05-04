using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFIreBallBehaviour : MonoBehaviour
{
    bool _lockFire;
    
    private void Start() 
    {
        Invoke("Death", 2f);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(!_lockFire)
        {
            _lockFire = true;
            if(other.TryGetComponent<EnemyBodyPart>(out EnemyBodyPart enemyBehaviour))
            {
                PlayerRoundStats.FireCompanionKills++;
                Death();
            }
        }
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
