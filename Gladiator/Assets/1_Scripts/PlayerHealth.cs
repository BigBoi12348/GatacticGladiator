using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth{get; private set;}

    private void Awake() 
    {
        currentHealth = maxHealth;
        //TakeDamage(20);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("EnemyWeapon"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            GameEvents.gameEndSetUp?.Invoke();
        }
    }
}
