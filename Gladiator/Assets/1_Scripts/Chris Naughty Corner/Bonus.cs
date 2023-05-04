using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static GameEvents;

public class Bonus : MonoBehaviour
{
    private enum BonusType
    {
        Sheild, Jump, Ability, Health
    }
    [SerializeField] private PlayerHealth _playerHealth;
    private BonusType _bonusType;
    private bool ICompletedBonus;
    

    private void OnEnable()
    {
        GameEvents.gameStartSetUp += ChooseBonus;
        GameEvents.gameEndSetUp += CheckToGiveBonus;
    }

    private void ChooseBonus()
    {
        int chance = Random.Range(1,5);
        switch (chance)
        {
            case 1: 
                _bonusType = BonusType.Sheild;
                break;
            case 2: 
                _bonusType = BonusType.Jump;
                break;
            case 3: 
                if(RoundData.Wave > 6)
                {
                    _bonusType = BonusType.Ability;
                }
                else
                {
                    _bonusType = BonusType.Health;
                }
                
                break;
            case 4: 
                _bonusType = BonusType.Health;
                break;
        }

        ICompletedBonus = true;
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.Alpha0))
    //     {
    //         Time.timeScale = 1;
    //     }
    //     if(_bonusType == BonusType.Sheild)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Mouse1))
    //         {
    //             ICompletedBonus = false;
    //         }
    //     }
    //     else if(_bonusType == BonusType.Jump)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Space))
    //         {
    //             ICompletedBonus = false;
    //         }
    //     }
    //     else if(_bonusType == BonusType.Ability)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
    //         {
    //             ICompletedBonus = false;
    //         }
    //     }
    //     else if(_bonusType == BonusType.Health)
    //     {
    //         if (_playerHealth.currentHealth > _playerHealth.maxHealth/2)
    //         {
    //             ICompletedBonus = false;
    //         }
    //     }
    // }

    private void CheckToGiveBonus(bool state)
    {
        // if(ICompletedBonus)
        // {
        //     switch (_bonusType)
        //     {
        //         case BonusType.Sheild:
        //             RoundData.PlayerPoints += 1;
        //             break;
        //         case BonusType.Jump:
        //             RoundData.PlayerPoints += 1;
        //             break;
        //         case BonusType.Ability:
        //             RoundData.PlayerPoints += 2;
        //             break;
        //         case BonusType.Health:
        //             RoundData.PlayerPoints += 2;
        //             break;
        //     }
        // }
    }   
}
