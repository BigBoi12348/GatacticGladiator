using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSceneManager : MonoBehaviour
{
    [SerializeField] private Button _exitButton;

    void Start()
    {
        _exitButton.onClick.AddListener(GoBackToGame);
    }
    
    private void GoBackToGame()
    {
        GameManager.Instance.LoadThisScene(1);
    }
}
