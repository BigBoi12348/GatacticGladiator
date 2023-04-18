using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth{get; private set;}
    public float poisin = 0.1f;
    bool dead;
  
    public bool TakeNoDamage;
    public bool TakeNoFireDamage;

    private void Awake() 
    {
        dead = false;
        maxHealth = RoundData.PlayerMaxHealth;
        currentHealth = maxHealth;
    }
   
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("EnemyWeapon"))
        {
            if(!TakeNoDamage)
            {
                PostProcessingEffectManager.Instance.HurtEffect(0.4f);
            }
            TakeDamage(20);
        }
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
    }
   
    public void TakeDamage(int damage)
    {
        if(!TakeNoDamage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0 && !dead)
            {
                dead = true;
                GameEvents.gameEndSetUp?.Invoke(false);
                Destroy(this);
            }
        }
    }

    public void TakeFireDamage(int damage)
    {
        if(!TakeNoFireDamage)
        {
            PostProcessingEffectManager.Instance.BurnEffect(0.1f);
            currentHealth -= damage;
            if (currentHealth <= 0 && !dead)
            {
                dead = true;
                GameEvents.gameEndSetUp?.Invoke(false);
                Destroy(this);
            }
        }
    }

    public void PoisonDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            //GameEvents.gameEndSetUp?.Invoke(false);
            Destroy(this);
        }
       
    }

}
