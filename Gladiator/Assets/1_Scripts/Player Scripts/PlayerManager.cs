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
    public float UpgradeFourBonus;
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
            Debug.Log("Geting called");
        }
    }

    private void Start() 
    {
        if(_playerAnim == null )
        {
            _playerAnim = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();
        }

        _playerAnim.speed = 1 + RoundData.ExtraPlayerAttackSpeed;
        if(PlayerUpgradesData.AttackOne)
        {
            _playerAnim.speed += 0.3f;
        }
        
        if(PlayerUpgradesData.AttackFour)
        {
            _playerAnim.speed += 0.5f;
        }

        if(PlayerUpgradesData.ShieldThree)
        {
            _firstPersonController.dashTotalCooldown = 2;
        }
        if(PlayerUpgradesData.ShieldFive)
        {
            _firstPersonController.walkSpeed += 5;
        }
        
        _playerShieldBehaviour.ReadyPlayerShield();
    }

    public void UpdatePlayerAttackSpeed(float value)
    {
        _playerAnim.speed += value;
    }
}
