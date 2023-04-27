using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static GameEvents;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerHealth _playerHealth;

    void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {
        GameEvents.gameEndSetUp += NoShieldBonus;
        GameEvents.gameEndSetUp += NoJumpBonus;
        GameEvents.gameEndSetUp += NoAbilitiesBonus;
        GameEvents.gameEndSetUp += PlayerHealthBonus;
    }
    // Update is called once per frame
    void Update()
    {
        //shiled bonus 
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            NoShieldBonus(false);
            Debug.Log("no");
        }
        else
        {
            NoShieldBonus(true);
            Debug.Log("++");
        }
        //kump bonus
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NoJumpBonus(false);
            Debug.Log("no");
        }
        else
        {
            NoJumpBonus(true);
            Debug.Log("+++");
        }
        //ABility bionua
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            NoAbilitiesBonus(false);
            Debug.Log("no");
        }
        else
        {
            NoAbilitiesBonus(true);
            Debug.Log("++++");
        }
        //Health Bonus
        if (_playerHealth.currentHealth = _playerHealth.maxHealth)
        {
            PlayerHealthBonus(false);
            Debug.Log("no");
        }
        else
        {
            PlayerHealthBonus(true);
            Debug.Log("+++++");
        }

    }
    private void NoShieldBonus(bool ShieldBonus)
    {
        
            InGameLevelManager.Instance.BonusCredits = 2;
          
       
    }
    private void NoJumpBonus(bool JumpBonus)
    {

        InGameLevelManager.Instance.BonusCredits = 2;


    }
    private void NoAbilitiesBonus(bool AbilitiesBonus)
    {

        InGameLevelManager.Instance.BonusCredits = 2;

    }
    private void PlayerHealthBonus(bool HealthBonus)
    {

        InGameLevelManager.Instance.BonusCredits = 2;



    }
}
