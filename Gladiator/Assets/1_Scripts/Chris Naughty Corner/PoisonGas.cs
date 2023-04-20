using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private float _tickInterval;
    private float nextTickTime;

    void Update()
    {
        if (nextTickTime < 0)
        {  
            nextTickTime = _tickInterval;
            _playerHealth.TakePoisonDamage(2);       
        }
        nextTickTime -= Time.deltaTime;
    }
}
