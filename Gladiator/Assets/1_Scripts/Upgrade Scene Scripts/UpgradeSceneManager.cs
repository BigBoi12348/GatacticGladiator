using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class UpgradeSceneManager : MonoBehaviour
{
    public static UpgradeSceneManager Instance;

    [Header("Information Board")]
    [SerializeField] private TMP_Text _upgradeTitle;
    [SerializeField] private VideoPlayer _upgradeVideoPlayer;
    [SerializeField] private TMP_Text _upgradeDescription;
    [SerializeField] private TMP_Text _upgradeComboBonus;
    [SerializeField] private TMP_Text _upgradeCost;
    [SerializeField] private Animator _informationBoardAnim;
    const string OPENINFOBOARD = "InformatioBoard_Open";
    const string CLOSEINFOBOARD = "InformatioBoard_Close";

    [Header("Buttons")]
    //[SerializeField] private Button _inventoryButton;
    //[SerializeField] private Button _upgradesButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _points;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        if(GameManager.Instance.LastSceneID == 0)
        {
            RoundData.PlayerPoints = 150;
        }
        
        Cursor.lockState = CursorLockMode.None;

        _exitButton.onClick.AddListener(GoBackToGame);
        _points.text = "Credits: " + RoundData.PlayerPoints.ToString();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            RoundData.PlayerPoints = 150;
            UpdateCurrentUIPlayerPoints();
        }
    }

    public void DisplayThisUpgradeSlot(string UpgradeName, VideoClip videoClip, string UpgradeDescription, string UpgradeComboBonus, string UpgradeCost, Color32 colorOfUpgradeCost)
    {
        _informationBoardAnim.Play(OPENINFOBOARD);
        _upgradeTitle.text = UpgradeName;
        _upgradeVideoPlayer.clip = videoClip;
        _upgradeDescription.text = UpgradeDescription;
        _upgradeComboBonus.text = UpgradeComboBonus;
        _upgradeCost.text = UpgradeCost;
        _upgradeCost.color = colorOfUpgradeCost;
    }

    public void HideInformationBoard()
    {
        if(_informationBoardAnim != null)
        {
            _informationBoardAnim.Play(CLOSEINFOBOARD);
        }
    }
    
    private void GoBackToGame()
    {
        GameManager.Instance.LoadThisScene(GameManager.GAMESCENE);
    }

    public void UpdateCurrentUIPlayerPoints()
    {
        _points.text = "Credits: " + RoundData.PlayerPoints.ToString();
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
                        PlayerUpgradesData.AttackOne = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackTwo = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackThree = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackFour = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 5:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.AttackFive = true;
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
                        PlayerUpgradesData.ShieldOne = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldTwo = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldThree = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldFour = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 5:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.ShieldFive = true;
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
                        PlayerUpgradesData.StarOne = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.StarTwo = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 3:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.StarThree = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 4:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.StarFour = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 5:
                    if(upGradeCost <= RoundData.PlayerPoints)
                    {
                        PlayerUpgradesData.StarFive = true;
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
