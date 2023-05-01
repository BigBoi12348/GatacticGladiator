using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillComboHandler : MonoBehaviour
{
    public static int KillComboCounter{get; private set;}
    [SerializeField] private PlayerManager _playerManager;
    [Header("Variables")]
    public float _killComboTotalTime;
    private float _killComboTimer;
    private bool _isKillComboOn;
    private bool ChanceToKillExtra;
    private bool _hasExtraComboChance;
    private int _extraComboChance;
    private bool _bonusAttackSpeed;
    bool firstChanged;
    bool secondChanged;
    [Header("UI")]
    [SerializeField] private GameObject _killComboParent;
    [SerializeField] private TMP_Text _killComboText;

    [Header("Animations")]
    [SerializeField] private Animator _killComboAnim;
    const string REOPEN = "KillCombo_Start";
    
    private void Start() 
    {
        if(PlayerUpgradesData.AttackOne)
        {
            _hasExtraComboChance = true;
        }
        if(PlayerUpgradesData.AttackTwo)
        {
            ChanceToKillExtra = true;
        }
        if(PlayerUpgradesData.AttackFour)
        {
            _bonusAttackSpeed = true;
        }
    }

    private void OnEnable() 
    {
        
    }

    private void OnDisable() 
    {
        
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            AddToCombo(30);
        }
        if(_hasExtraComboChance)
        {
            if(KillComboHandler.KillComboCounter >= 30)
            {               
                _extraComboChance = 15;
            }
            else
            {
                _extraComboChance = 0;
            }
        }

        if(_bonusAttackSpeed)
        {
            if(KillComboHandler.KillComboCounter >= 60)
            {
                if(!secondChanged)
                {
                    _playerManager.UpdatePlayerAttackSpeed(0.3f);
                    firstChanged = false;
                    secondChanged = true;
                }
            }
            else if(KillComboHandler.KillComboCounter >= 20)
            {
                if(!firstChanged)
                {
                    if(secondChanged)
                    {
                        _playerManager.UpdatePlayerAttackSpeed(-0.3f);
                    }
                    else
                    {
                        _playerManager.UpdatePlayerAttackSpeed(0.1f);
                    }
                    firstChanged = true;
                    secondChanged = false;
                }
            }
        }
        
        _killComboText.text = KillComboCounter.ToString();
        if(_killComboTimer < 0)
        {
            KillComboCounter = 0;
            _killComboParent.SetActive(false);
        }
        _killComboTimer -= Time.deltaTime;

        if(KillComboHandler.KillComboCounter > PlayerRoundStats.HighestComboRetained)
        {
            PlayerRoundStats.HighestComboRetained = KillComboHandler.KillComboCounter;
        }
    }

    public void AddToCombo(int value)
    {
        if(KillComboCounter == 0)
        {
            _killComboParent.SetActive(true);
        }
        KillComboCounter += value;
        _killComboTimer = _killComboTotalTime;
        _killComboAnim.Play(REOPEN, 0, 0);

        if(ChanceToKillExtra)
        {
            int ranChance = Random.Range(1,101);
            if(ranChance <= 20 + _extraComboChance)
            {
                KillComboCounter += 1;
                StartCoroutine(FlashTextRed());
            }
        }
    }

    public void GivePlayerAttackSpeed(float x)
    {
        _playerManager.UpdatePlayerAttackSpeed(x);
    }

    public static void RemoveCombo(int value)
    {
        if(KillComboCounter > 0)
        {
            KillComboCounter -= value;
        }
    }

    public static void SetCombo(int value)
    {
        KillComboCounter = value;
    }
    
    IEnumerator FlashTextRed()
    {
        _killComboText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _killComboText.color = Color.white;
    }
}
