using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
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


    [Header("Information")]
    [SerializeField] private string _upgradeName;
    [SerializeField] private VideoClip _videoOfUpgrade;
    [TextArea(1,5)]
    [SerializeField] private string _upgradeDescription;
    [TextArea(1,4)]
    [SerializeField] private string _upgradeComboBonus;
    private string _upgradeCostString;


    [Header("UI Elements")]
    [SerializeField] private Button _upgradeSlotButton;
    [SerializeField] private RawImage _imageSymbol;
    [Range(1f, 2f)]
    [SerializeField] private float IncreaseHoverImageSize;
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private GameObject _hoverUI;
    [SerializeField] private GameObject _alreadyBoughtUI;
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
            switch (_thisUpgradeRank)
            {
                case 1:
                    if(PlayerUpgradesData.AttackOne)
                    {
                        LockMe();
                    }
                    break;
                case 2:
                    if(PlayerUpgradesData.AttackTwo)
                    {
                        LockMe();
                    }
                    break;
                case 3:
                    if(PlayerUpgradesData.AttackThree)
                    {
                        LockMe();
                    }
                    break;
                case 4:
                    if(PlayerUpgradesData.AttackFour)
                    {
                        LockMe();
                    }
                    break;
                case 5:
                    if(PlayerUpgradesData.AttackFive)
                    {
                        LockMe();
                    }
                    break;
            }
        }
        else if(_upgradeType == UpgradeType.Shield)
        {
            switch (_thisUpgradeRank)
            {
                case 1:
                    if(PlayerUpgradesData.ShieldOne)
                    {
                        LockMe();
                    }
                    break;
                case 2:
                    if(PlayerUpgradesData.ShieldTwo)
                    {
                        LockMe();
                    }
                    break;
                case 3:
                    if(PlayerUpgradesData.ShieldThree)
                    {
                        LockMe();
                    }
                    break;
                case 4:
                    if(PlayerUpgradesData.ShieldFour)
                    {
                        LockMe();
                    }
                    break;
                case 5:
                    if(PlayerUpgradesData.ShieldFive)
                    {
                        LockMe();
                    }
                    break;
            }
        }
        else if(_upgradeType == UpgradeType.Ability)
        {
            switch (_thisUpgradeRank)
            {
                case 1:
                    if(PlayerUpgradesData.StarOne)
                    {
                        LockMe();
                    }
                    break;
                case 2:
                    if(PlayerUpgradesData.StarTwo)
                    {
                        LockMe();
                    }
                    break;
                case 3:
                    if(PlayerUpgradesData.StarThree)
                    {
                        LockMe();
                    }
                    break;
                case 4:
                    if(PlayerUpgradesData.StarFour)
                    {
                        LockMe();
                    }
                    break;
                case 5:
                    if(PlayerUpgradesData.StarFive)
                    {
                        LockMe();
                    }
                    break;
            }
        }

        if(_upgradeCostString == null)
        {
            _upgradeCostString = _upgradeCost.ToString();
        }
    }


    public void HoverOn()
    {
        UpgradeSceneManager.Instance.DisplayThisUpgradeSlot(_upgradeName, _videoOfUpgrade, _upgradeDescription, _upgradeComboBonus, _upgradeCostString);
        _hoverUI.SetActive(true);
        _imageSymbol.transform.localScale = new Vector3(IncreaseHoverImageSize, IncreaseHoverImageSize, IncreaseHoverImageSize);
    }

    public void HoverOff()
    {
        _hoverUI.SetActive(false);
        UpgradeSceneManager.Instance.HideInformationBoard();
        _imageSymbol.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void TryToUnlockMe()
    {
        if(!alreadyBought)
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

    // private bool CheckIfInOrder()
    // {
    //     if(_upgradeType == UpgradeType.Sword)
    //     {
    //         if(PlayerUpgradesData.AttackAttribute == _thisUpgradeRank-1)
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     else if(_upgradeType == UpgradeType.Shield)
    //     {
    //         if(PlayerUpgradesData.ShieldAttribute == _thisUpgradeRank-1)
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     else if(_upgradeType == UpgradeType.Ability)
    //     {
    //         if(PlayerUpgradesData.AbilityAttribute == _thisUpgradeRank-1)
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     return false;
    // }

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
