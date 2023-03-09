using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSceneManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private Button _upgradesButton;
    [SerializeField] private Button _exitButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        _inventoryButton.onClick.AddListener(GoToInventoryScreen);
        _upgradesButton.onClick.AddListener(GoToUpgradesScreen);
        _exitButton.onClick.AddListener(GoBackToGame);
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
