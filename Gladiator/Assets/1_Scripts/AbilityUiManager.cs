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
    }

    void Update()
    {
        _firstAbilitySlider.value = _firstPersonController.dashCooldownTimer;
        _firstTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
        if(secondOpen)
        {
            _secondAbilitySlider.value = _firstPersonController.dashCooldownTimer;
            _secondTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
        }
        if(thirdOpen)
        {
            _thirdAbilitySlider.value = _firstPersonController.dashCooldownTimer;
            _thirdTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
        }
        if(fourthOpen)
        {
            _fourthAbilitySlider.value = _firstPersonController.dashCooldownTimer;
            _fourthTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
        }
        if(fifthOpen)
        {
            _fifthAbilitySlider.value = _firstPersonController.dashCooldownTimer;
            _fifthTextTimer.text = Mathf.RoundToInt(_firstPersonController.dashCooldownTimer).ToString();
        }
    }
}
