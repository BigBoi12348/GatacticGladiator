using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStopperBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    [SerializeField] private LayerMask _wallLayer;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.layer == _wallLayer)
        {
            enemyBehaviour.StartDeath();
        }
    }
}
