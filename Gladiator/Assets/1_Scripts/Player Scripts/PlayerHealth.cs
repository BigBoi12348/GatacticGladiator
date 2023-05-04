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
    public bool TakeNoPoisonDamage;
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
        if(PlayerUpgradesData.ShieldThree)
        {
            _extraHealth += 20;
        }
        if(PlayerUpgradesData.ShieldFive)
        {
            _extraHealth += 50;
        }
        dead = false;
        maxHealth = RoundData.PlayerMaxHealth + _extraHealth;
        currentHealth = maxHealth;
    }

    public void AddHealth(int value)
    {
        if(currentHealth + value < maxHealth)
        {
            currentHealth += value;
            PlayerRoundStats.DamageHealed += value;
        }
        else
        {
            currentHealth = maxHealth;
            PlayerRoundStats.DamageHealed += maxHealth - value;
        }
    }
   
    public void TakeDamage(int damage)
    {
        if(!TakeNoDamage)
        {
            PostProcessingEffectManager.Instance.HurtEffect(0.1f);
            if(AmIABeast)
            {
                if(KillComboHandler.KillComboCounter >= 175)
                {
                    if(currentHealth - damage < maxHealth*0.1f)
                    {
                        damage = 0;
                        currentHealth = maxHealth/20;
                        KillComboHandler.RemoveCombo(1);
                    }
                }
            }
            if(AmIResilient)
            {
                damage -= 1;
            }
            currentHealth -= damage;
            PlayerRoundStats.DamageTaken += damage;
            if (currentHealth <= 0 && !dead)
            {
                if(!InGameLevelManager.RoundIsOver)
                {
                    InGameLevelManager.RoundIsOver = true;
                    dead = true;
                    GameEvents.gameEndSetUp?.Invoke(false);
                    Destroy(this);
                }
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
                if(KillComboHandler.KillComboCounter >= 90)
                {
                    if(currentHealth - damage < maxHealth*0.2f)
                    {
                        damage = 0;
                        currentHealth = maxHealth*0.2f;
                    }
                }
            }
            
            currentHealth -= damage;
            PlayerRoundStats.DamageTaken += damage;
            if (currentHealth <= 0 && !dead)
            {
                if(!InGameLevelManager.RoundIsOver)
                {
                    InGameLevelManager.RoundIsOver = true;
                    dead = true;
                    GameEvents.gameEndSetUp?.Invoke(false);
                    Destroy(this);
                }
            }
        }
    }

    public void TakePoisonDamage(int damage)
    {
        if(!TakeNoPoisonDamage)
        {
            if(AmIResilient)
            {
                if(KillComboHandler.KillComboCounter >= 90)
                {
                    if(currentHealth - damage < maxHealth*0.2f)
                    {
                        damage = 0;
                        currentHealth = maxHealth*0.2f;
                    }
                }
            }
        }

        currentHealth -= damage;
        PlayerRoundStats.DamageTaken += damage;
        if (currentHealth <= 0 && !dead)
        {
            if(!InGameLevelManager.RoundIsOver)
            {
                InGameLevelManager.RoundIsOver = true;
                dead = true;
                GameEvents.gameEndSetUp?.Invoke(false);
                Destroy(this);
            } 
        }  
    }
}
