using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFIreBallBehaviour : MonoBehaviour
{
    private void Start() 
    {
        Invoke("Death", 2f);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
        {
            PlayerRoundStats.FireCompanionKills++;
            Death();
        }
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
