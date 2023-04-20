using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FirstPersonController _firstPersonController;

    [Header("UI Objects")]
    [SerializeField] private GameObject _firstAbility;
    [SerializeField] private GameObject _secondAbility;
    [SerializeField] private GameObject _thirdAbility;
    [SerializeField] private GameObject _fourthAbility;
    [SerializeField] private GameObject _fifthAbility;
    bool secondOpen;
    bool thirdOpen;
    bool fourthOpen;
    bool fifthOpen;

    [Header("Ability UI")]
    [SerializeField] private Slider _firstAbilitySlider;
    [SerializeField] private TMP_Text _firstTextTimer;
    [SerializeField] private Slider _secondAbilitySlider;
    [SerializeField] private TMP_Text _secondTextTimer;
    [SerializeField] private Slider _thirdAbilitySlider;
    [SerializeField] private TMP_Text _thirdTextTimer;
    [SerializeField] private Slider _fourthAbilitySlider;
    [SerializeField] private TMP_Text _fourthTextTimer;
    [SerializeField] private Slider _fifthAbilitySlider;
    [SerializeField] private TMP_Text _fifthTextTimer;

    void Start()
    {
        _secondAbility.SetActive(false);
        _thirdAbility.SetActive(false);
        _fourthAbility.SetActive(false);
        _fifthAbility.SetActive(false);

        if(PlayerUpgradesData.StarTwo)
        {
            _secondAbility.SetActive(true);
            secondOpen = true;
        }
        if(PlayerUpgradesData.StarThree)
        {
            _thirdAbility.SetActive(true);
            thirdOpen = true;
        }
        if(PlayerUpgradesData.StarFour)
        {
            _fourthAbility.SetActive(true);
            fourthOpen = true;
        }
        if(PlayerUpgradesData.StarFive)
        {
            _fifthAbility.SetActive(true);
            fifthOpen = true;
        }

        if(_firstPersonController == null)
        {
            _firstPersonController = FindObjectOfType<FirstPersonController>();
        }

        _firstAbilitySlider.maxValue = _firstPersonController.GetTotalDashTime;
        _secondAbilitySlider.maxValue = _firstPersonController.GetTotalForceFieldTime;
        _thirdAbilitySlider.maxValue = _firstPersonController.GetTotalFireBeamsTime;
        _fourthAbilitySlider.maxValue = _firstPersonController.GetTotalGravityPoundTime;
        _fifthAbilitySlider.maxValue = _firstPersonController.GetTotalThanosSnapTime;
    }

    void Update()
    {
        if(_firstPersonController.dashCooldownTimer > 0)
        {
            _firstAbilitySlider.value = _firstPersonController.dashCooldownTimer;
            _firstTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
            _firstTextTimer.enabled = true;
        }
        else
        {
            _firstTextTimer.enabled = false;
        }
        if(secondOpen)
        {
            if(_firstPersonController._forceFieldTimer > 0)
            {
                _secondAbilitySlider.value = _firstPersonController._forceFieldTimer;
                _secondTextTimer.text = Mathf.RoundToInt(_firstPersonController._forceFieldTimer).ToString();
                _secondTextTimer.enabled = true;
            }
            else
            {
                _secondTextTimer.enabled = false;
            }
        }
        if(thirdOpen)
        {
            if(_firstPersonController._fireBeamsTimer > 0)
            {
                _thirdAbilitySlider.value = _firstPersonController._fireBeamsTimer;
                _thirdTextTimer.text = Mathf.RoundToInt(_firstPersonController._fireBeamsTimer).ToString();
                _thirdTextTimer.enabled = true;
            }
            else
            {
                _thirdTextTimer.enabled = false;
            }
        }
        if(fourthOpen)
        {
            if(_firstPersonController._gravityPoundTimer > 0)
            {
                _fourthAbilitySlider.value = _firstPersonController._gravityPoundTimer;
                _fourthTextTimer.text = Mathf.RoundToInt(_firstPersonController._gravityPoundTimer).ToString();
                _fourthTextTimer.enabled = true;
            }
            else
            {
                _fourthTextTimer.enabled = false;
            }
        }
        if(fifthOpen)
        {
            if(_firstPersonController._thanosSnapTimer > 0)
            {
                _fifthAbilitySlider.value = _firstPersonController._thanosSnapTimer;
                _fifthTextTimer.text = Mathf.RoundToInt(_firstPersonController._thanosSnapTimer).ToString();
                _fourthTextTimer.enabled = true;
            }
            else
            {
                _fourthTextTimer.enabled = false;
            }
        }
    }
}
