using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
