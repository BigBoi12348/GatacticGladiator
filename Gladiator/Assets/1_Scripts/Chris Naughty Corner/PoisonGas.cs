using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    public float damagePerTick = 10f;
    public float tickInterval = 1f;
    public float radius = 1f;

    private float nextTickTime;

    void Update()
    {
        if (Time.time >= nextTickTime)
        {
            nextTickTime = Time.time + tickInterval;

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider collider in colliders)
            {
                PlayerHealth health = collider.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(1);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
