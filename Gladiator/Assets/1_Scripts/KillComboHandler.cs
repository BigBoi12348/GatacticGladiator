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

    [Header("UI")]
    [SerializeField] private GameObject _killComboParent;
    [SerializeField] private TMP_Text _killComboText;

    [Header("Animations")]
    [SerializeField] private Animator _killComboAnim;
    const string REOPEN = "KillCombo_Start";
    
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
    }
}
