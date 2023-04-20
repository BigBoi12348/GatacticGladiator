using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private PlayerShieldBehaviour _playerShieldBehaviour;
    [SerializeField] private KillComboHandler _killComboHandler;
    [SerializeField] private FirstPersonController _firstPersonController;

    private void Awake() 
    {
        if(_playerAnim )
        {

        }
        _firstPersonController.enabled = true;
    }

    private void OnEnable() 
    {
        GameEvents.gameEndSetUp += EndGameSetUp;
    }

    private void OnDisable() 
    {
        GameEvents.gameEndSetUp -= EndGameSetUp;
    }

    private void EndGameSetUp(bool didPlayerWin)
    {
        if(!didPlayerWin)
        {
            _firstPersonController.enabled = false;
        }
    }

    private void Start() 
    {
        if(_playerAnim == null )
        {
            _playerAnim = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
        }

        _playerAnim.speed = 1;
        if(PlayerUpgradesData.AttackOne)
        {
            _playerAnim.speed += 0.3f;
        }
        
        if(PlayerUpgradesData.AttackFour)
        {
            _playerAnim.speed += 0.5f;
        }

        if(PlayerUpgradesData.ShieldFive)
        {
            _firstPersonController.walkSpeed += 4;
            _firstPersonController.sprintSpeed += 5;
        }

        // switch (PlayerUpgradesData.AttackAttribute)
        // {
        //     case 0:
        //         _playerAnim.speed = 1f;
        //         break;
        //     case 1:
        //         _playerAnim.speed = 1.25f;
        //         break;
        //     case 2:
        //         _playerAnim.speed = 1.5f;
        //         break;
        //     case 3:
        //         _playerAnim.speed = 1.75f;
        //         break;
        //     case 4:
        //         _playerAnim.speed = 2f;
        //         break;
        //     case 5:
        //         _playerAnim.speed = 2f;
        //         break;
           
        // }

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
        _playerShieldBehaviour.ReadyPlayerShield();
    }
}
