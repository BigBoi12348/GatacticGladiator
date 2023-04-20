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

    [Header("Ability UI")]
    [SerializeField] private Slider _firstAbilitySlider;
    [SerializeField] private TMP_Text _firstTextTimer;

    void Start()
    {
        _secondAbility.SetActive(false);
        _thirdAbility.SetActive(false);
        _fourthAbility.SetActive(false);
        _fifthAbility.SetActive(false);

        if(PlayerUpgradesData.StarTwo)
        {
            _secondAbility.SetActive(true);
        }
        if(PlayerUpgradesData.StarThree)
        {
            _thirdAbility.SetActive(true);
        }
        if(PlayerUpgradesData.StarFour)
        {
            _fourthAbility.SetActive(true);
        }
        if(PlayerUpgradesData.StarFive)
        {
            _fifthAbility.SetActive(true);
        }

        if(_firstPersonController == null)
        {
            _firstPersonController = FindObjectOfType<FirstPersonController>();
        }

        _firstAbilitySlider.maxValue = _firstPersonController.GetTotalDashTime;
    }

    void Update()
    {
        _firstAbilitySlider.value = _firstPersonController.dashCooldownTimer;
        _firstTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
    }
}
