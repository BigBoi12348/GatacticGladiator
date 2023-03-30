using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private PlayerShieldBehaviour playerShieldBehaviour;
    private void Awake() 
    {
        if(_playerAnim )
        {

        }
    }

    private void Start() 
    {
        if(_playerAnim == null )
        {
            _playerAnim = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
        }

        switch (PlayerUpgradesData.AttackAttribute)
        {
            case 0:
                _playerAnim.speed = 1f;
                break;
            case 1:
                _playerAnim.speed = 1.25f;
                break;
            case 2:
                _playerAnim.speed = 1.5f;
                break;
            case 3:
                _playerAnim.speed = 1.75f;
                break;
            case 4:
                _playerAnim.speed = 2f;
                break;
            case 5:
                _playerAnim.speed = 2f;
                break;
           
        }

        switch (PlayerUpgradesData.ShieldAttribute)
        {
            case 0:
                RoundData.SheildTotalEnergy = 100;
                break;
            case 1:
                RoundData.SheildTotalEnergy = 125;
                break;
            case 2:
                RoundData.SheildTotalEnergy = 150;
                break;
            case 3:
                RoundData.SheildTotalEnergy = 175;
                break;
            case 4:
                RoundData.SheildTotalEnergy = 200;
                break;
            case 5:
                RoundData.SheildTotalEnergy = 200;
                break;
           
        }
        playerShieldBehaviour.ReadyPlayerShield();
    }
}
