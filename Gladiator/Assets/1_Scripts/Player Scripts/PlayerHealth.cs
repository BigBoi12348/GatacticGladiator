using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth{get; private set;}
    public float poisin = 0.1f;
    bool dead;
  
    public bool TakeNoDamage;

    private void Awake() 
    {
        dead = false;
        currentHealth = maxHealth;
    }
   
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("EnemyWeapon"))
        {
            TakeDamage(20);
        }
        //if (other.gameObject.CompareTag("Poison"))
        //{
        //    TakeDamage(1);
        //}
        

    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("PoisonFogLevel"))
        {

            //TakeDamage(1);
            PoisonDamage(0.03f);
        }
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
