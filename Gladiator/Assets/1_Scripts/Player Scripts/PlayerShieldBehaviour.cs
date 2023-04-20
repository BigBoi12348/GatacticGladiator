using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldBehaviour : MonoBehaviour
{
    private Blocking blocking;
    

    [Header("Energy ")]
    [SerializeField] private float _currentEnergy;
    public float MaxTotalEnergy{private get; set;}
    [SerializeField] private float _setChargeRate;
    private float _chargeRateMutli;
    private float _chargeRate;
    [SerializeField] private float _decreaseRate;
    

    [Header("Shield Variables")]
    public bool _isBlocking;
    private bool _shieldLocked;
    [SerializeField] private Slider _energyShieldSlider;
    [SerializeField] private Slider _otherEnergyShieldSlider;


    [Header("Shield Recharge")]
    private bool _doesSheildHaveRecharge;
    private bool _secondLock;

    private void Start() 
    {
        if(PlayerUpgradesData.ShieldOne)
        {
            _chargeRate = _setChargeRate;
        }
    }

    public void Init(Blocking tempBlocking)
    {
        blocking = tempBlocking;
    }

    private void OnEnable() 
    {
        //GameEvents.playerStartGame += ReadyPlayerShield;
    }

    private void OnDisable() 
    {
        //GameEvents.playerStartGame -= ReadyPlayerShield;
    }

    public void ReadyPlayerShield() 
    {
        MaxTotalEnergy = RoundData.SheildTotalEnergy;
        _currentEnergy = MaxTotalEnergy;
        _energyShieldSlider.maxValue = MaxTotalEnergy;
        _energyShieldSlider.value = _currentEnergy;
        _otherEnergyShieldSlider.maxValue = MaxTotalEnergy;
        _otherEnergyShieldSlider.value = _currentEnergy;
    }

    private void Update() 
    {
        if (_isBlocking)
        {
            _secondLock = false;
            if (_currentEnergy > 0)
            {
                _currentEnergy -= Time.deltaTime*_decreaseRate;
            }
            else
            {
                StartCoroutine(blocking.AllowShieldBlockAgain());
            }
        }
        else if (_currentEnergy < MaxTotalEnergy)
        {
            _currentEnergy += Time.deltaTime*(_chargeRate*_chargeRateMutli);
        }
        else if (_currentEnergy >= MaxTotalEnergy && !_secondLock)
        {
            _secondLock = true;
            _currentEnergy = MaxTotalEnergy;
            StartCoroutine(blocking.AllowShieldBlockAgain());
        }

        _energyShieldSlider.value = _currentEnergy;
        _otherEnergyShieldSlider.value = _currentEnergy;
        
        if(PlayerUpgradesData.ShieldOne)
        {
            if(KillComboHandler.KillComboCounter >= 20)
            {
                _chargeRateMutli = 2;
            }
            else
            {
                _chargeRateMutli = 1;
            }
        }
    }

    public void AddRecharge(int value)
    {
        if(_currentEnergy + value <= MaxTotalEnergy)
        {
            _currentEnergy += value;
        }
        else
        {
            _currentEnergy = MaxTotalEnergy;
        }
    }
}
