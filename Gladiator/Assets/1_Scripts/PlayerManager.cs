using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _playerAnim;


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
            
                break;
           
        }
    }
}
