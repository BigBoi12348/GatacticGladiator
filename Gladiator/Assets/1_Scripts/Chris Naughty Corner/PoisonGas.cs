using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    public float damagePerSecond = 10.0f; // Amount of damage to apply per second
    public float damageInterval = 1.0f; // Interval in seconds between damage ticks

    private float timeSinceLastDamage = 0.0f;

    private void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(Mathf.RoundToInt(damagePerSecond * damageInterval));
                    Debug.Log("Player health: " + playerHealth.currentHealth); // Debug statement
                }
                timeSinceLastDamage -= damageInterval;
            }
        }
    }
}
