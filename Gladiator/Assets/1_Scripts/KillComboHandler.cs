using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillComboHandler : MonoBehaviour
{
    public static int KillComboCounter{get; private set;}
    
    [Header("Variables")]
    public float _killComboTotalTime;
    private float _killComboTimer;
    private bool _isKillComboOn;
    private bool ChanceToKillExtra;

    [Header("UI")]
    [SerializeField] private GameObject _killComboParent;
    [SerializeField] private TMP_Text _killComboText;

    [Header("Animations")]
    [SerializeField] private Animator _killComboAnim;
    const string REOPEN = "KillCombo_Start";
    
    private void Start() 
    {
        if(PlayerUpgradesData.AttackTwo)
        {
            ChanceToKillExtra = true;
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
        _killComboText.text = KillComboCounter.ToString();
        if(_killComboTimer < 0)
        {
            KillComboCounter = 0;
            _killComboParent.SetActive(false);
        }
        _killComboTimer -= Time.deltaTime;
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
            if(ranChance <= 75)
            {
                KillComboCounter += 1;
                StartCoroutine(FlashTextRed());
            }
        }
    }

    public static void RemoveCombo(int value)
    {
        if(KillComboCounter > 0)
        {
            KillComboCounter -= value;
        }
    }
    
    IEnumerator FlashTextRed()
    {
        _killComboText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _killComboText.color = Color.white;
    }
}
