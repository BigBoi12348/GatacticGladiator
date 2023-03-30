using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    public float damageAmount = 10f;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                int damageInt = Mathf.RoundToInt(damageAmount);
                playerHealth.TakeDamage(damageInt);
                Debug.Log("Player took " + damageAmount + " damage from particles.");
            }
        }
    }
}
