using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDmg : MonoBehaviour
{
    public int damageAmount = 10;
    //public string damageTag = "Projectile";
    public GameObject arrow;

    private void OnTriggerEnter(Collider other)
    {
        if (arrow)
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(0);
                Debug.Log("take");
            }
        }
    }
}
