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
    [SerializeField] private float _chargeRate;
    [SerializeField] private float _decreaseRate;
    

    [Header("Shield Variables")]
    public bool _isBlocking;
    private bool _shieldLocked;
    [SerializeField] private Slider _energyShieldSlider;
    [SerializeField] private Slider _otherEnergyShieldSlider;

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
        if(_isBlocking)
        {
            if(_currentEnergy > 0)
            {
                _currentEnergy -= Time.deltaTime*_decreaseRate;
            }
            else
            {
                _isBlocking = false;
            }
        }
        else if (!_shieldLocked)
        {
            _currentEnergy = MaxTotalEnergy;
            StartCoroutine(blocking.AllowShieldBlockAgain());
            _shieldLocked = true;
        }
        else if(_currentEnergy < MaxTotalEnergy)
        {
            _currentEnergy += Time.deltaTime*_chargeRate;
        }
        else if(_currentEnergy > 10)
        {
            _shieldLocked = false;
        }
        _energyShieldSlider.value = _currentEnergy;
        _otherEnergyShieldSlider.value = _currentEnergy;
    }
}
