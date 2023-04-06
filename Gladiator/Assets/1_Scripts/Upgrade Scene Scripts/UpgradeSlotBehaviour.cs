using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeSlotBehaviour : MonoBehaviour
{
    public enum UpgradeType
    {
        Sword, Shield, Ability
    }
    
    [Header("Details")]
    [SerializeField] private UpgradeType _upgradeType;
    [Range(1,5)]
    [SerializeField] private int _thisUpgradeRank;
    [SerializeField] private int _upgradeCost;


    [Header("UI Elements")]
    [SerializeField] private Button _upgradeSlotButton;
    [SerializeField] private RawImage _imageSymbol;
    [Range(1f, 2f)]
    [SerializeField] private float IncreaseHoverImageSize;
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private GameObject _hoverUI;
    [SerializeField] private GameObject _alreadyBoughtUI;
    [SerializeField] private GameObject _infoBoxUI;
    [SerializeField] private AudioSource _boughtSound;


    [Header("Images to choose from")]
    [SerializeField] private Texture _swordImage;
    [SerializeField] private Texture _shieldImage;
    [SerializeField] private Texture _abilityImage;

    bool alreadyBought;

    private void Awake() 
    {
        alreadyBought = false;

        if(_upgradeType == UpgradeType.Sword)
        {
            _imageSymbol.texture = _swordImage;

            if(_thisUpgradeRank <= PlayerUpgradesData.AttackAttribute)
            {
                LockMe();
            }
        }
        else if(_upgradeType == UpgradeType.Shield)
        {
            _imageSymbol.texture = _shieldImage;

            if(_thisUpgradeRank <= PlayerUpgradesData.ShieldAttribute)
            {
                LockMe();
            }
        }
        else if(_upgradeType == UpgradeType.Ability)
        {
            _imageSymbol.texture = _abilityImage;

            if(_thisUpgradeRank <= PlayerUpgradesData.AbilityAttribute)
            {
                LockMe();
            }
        }

    }

    public void HoverOn()
    {
        _hoverUI.SetActive(true);
        _infoBoxUI.SetActive(true);
        _imageSymbol.transform.localScale = new Vector3(IncreaseHoverImageSize, IncreaseHoverImageSize, IncreaseHoverImageSize);
    }

    public void HoverOff()
    {
        _hoverUI.SetActive(false);
        _infoBoxUI.SetActive(false);
        _imageSymbol.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void TryToUnlockMe()
    {
        if(!alreadyBought && CheckIfInOrder())
        {
            bool didBuy = UpgradeSceneManager.Instance.TryUpgradeThisTier(_thisUpgradeRank, _upgradeCost, _upgradeType);

            if(didBuy)
            {
                RoundData.PlayerPoints -= _upgradeCost;
                UpgradeSceneManager.Instance.UpdateCurrentUIPlayerPoints();
                LockMe();
            }
            else
            {
                //Display something to let the player know they couldnt buy it
            }
        }
    }

    private bool CheckIfInOrder()
    {
        if(_upgradeType == UpgradeType.Sword)
        {
            if(PlayerUpgradesData.AttackAttribute == _thisUpgradeRank-1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(_upgradeType == UpgradeType.Shield)
        {
            if(PlayerUpgradesData.ShieldAttribute == _thisUpgradeRank-1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(_upgradeType == UpgradeType.Ability)
        {
            if(PlayerUpgradesData.AbilityAttribute == _thisUpgradeRank-1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void LockMe()
    {
        alreadyBought = true;
        _alreadyBoughtUI.SetActive(true);
        _boughtSound.Play();

        HoverOff();

        _upgradeSlotButton.enabled = false;
        eventTrigger.enabled = false;
    }
}
