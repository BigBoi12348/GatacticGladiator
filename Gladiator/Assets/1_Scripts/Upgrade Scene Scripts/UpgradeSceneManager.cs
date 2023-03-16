using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSceneManager : MonoBehaviour
{
    public static UpgradeSceneManager Instance;

    [Header("Buttons")]
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _upgradesButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _points;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Cursor.lockState = CursorLockMode.None;
        _inventoryButton.onClick.AddListener(GoToInventoryScreen);
        _upgradesButton.onClick.AddListener(GoToUpgradesScreen);
        _exitButton.onClick.AddListener(GoBackToGame);
        _points.text = "Points: " + RoundData.PlayerPoints.ToString();
    }

    private void GoToInventoryScreen()
    {
        ViewManager.Show<InventoryView>(0);
    }

    private void GoToUpgradesScreen()
    {
        ViewManager.Show<UpgradesView>(0);
    }
    
    private void GoBackToGame()
    {
        GameManager.Instance.LoadThisScene(1);
    }

    public void UpdateCurrentUIPlayerPoints()
    {
        _points.text = "Points: " + RoundData.PlayerPoints.ToString();
    }

    public bool TryUpgradeThisTier(int upGradeRank ,int upGradeCost, UpgradeSlotBehaviour.UpgradeType upgradeType)
    {
        if(upgradeType == UpgradeSlotBehaviour.UpgradeType.Sword)
        {
            switch (upGradeRank)
            {
                case 1:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 5:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        else if(upgradeType == UpgradeSlotBehaviour.UpgradeType.Shield)
        {
            switch (upGradeRank)
            {
                case 1:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 5:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        else if(upgradeType == UpgradeSlotBehaviour.UpgradeType.Ability)
        {
            switch (upGradeRank)
            {
                case 1:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AbilityAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AbilityAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AbilityAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AbilityAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 5:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AbilityAttribute = upGradeRank;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        return false;
    }
}
