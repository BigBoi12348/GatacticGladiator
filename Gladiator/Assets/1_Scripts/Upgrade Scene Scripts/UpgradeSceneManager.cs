using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSceneManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _upgradesButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _points;

    void Start()
    {
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
}
