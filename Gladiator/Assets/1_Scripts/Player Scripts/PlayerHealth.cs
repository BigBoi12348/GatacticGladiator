using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth{get; private set;}
    bool dead;
    private bool AmIResilient;
    private bool AmIABeast;
    public bool TakeNoDamage;
    public bool TakeNoFireDamage;
    private int _extraHealth = 0;
    private void Awake() 
    {
        if(PlayerUpgradesData.ShieldTwo)
        {
            AmIResilient = true;
        }
        else
        {
            AmIResilient = false;
        }
        if(PlayerUpgradesData.ShieldFive)
        {
            AmIABeast = true;
        }
        else
        {
            AmIABeast = false;
        }
        if(PlayerUpgradesData.ShieldFive)
        {
            _extraHealth = 50;
        }
        dead = false;
        maxHealth = RoundData.PlayerMaxHealth + _extraHealth;
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
            PostProcessingEffectManager.Instance.HurtEffect(0.1f);
            if(AmIABeast)
            {
                if(KillComboHandler.KillComboCounter >= 75)
                {
                    if(currentHealth - damage < maxHealth*0.1f)
                    {
                        damage = 0;
                        currentHealth = maxHealth/20;
                        KillComboHandler.RemoveCombo(1);
                    }
                }
            }

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
            if(AmIResilient)
            {
                if(KillComboHandler.KillComboCounter >= 45)
                {
                    if(currentHealth - damage < maxHealth*0.2f)
                    {
                        damage = 0;
                        currentHealth = maxHealth*0.2f;
                    }
                }
            }
            
            currentHealth -= damage;
            if (currentHealth <= 0 && !dead)
            {
                dead = true;
                GameEvents.gameEndSetUp?.Invoke(false);
                Destroy(this);
            }
        }
    }

    public void TakePoisonDamage(float damage)
    {
        if(AmIResilient)
        {
            if(KillComboHandler.KillComboCounter >= 45)
            {
                if(currentHealth - damage < maxHealth*0.2f)
                {
                    damage = 0;
                    currentHealth = maxHealth*0.2f;
                }
            }
        }
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            //GameEvents.gameEndSetUp?.Invoke(false);
            Destroy(this);
        }  
    }
}
