using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Player Health UI")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private GameObject _poisonText;

    [Header("Texts")]
    [SerializeField] private TMP_Text _mainWaveText;
    [SerializeField] private TMP_Text _subWaveText;
    [SerializeField] private TMP_Text _enemiesLeftText;
    [SerializeField] private GameObject _waveComplete;
    [SerializeField] private GameObject _playerLost;


    [Header("Animations")]
    [SerializeField] private Animator _whiteScreenAnim;
    [SerializeField] private AnimationClip _waveInClip;
    const string FLASHWHITE = "WhiteScreen";
    private void OnEnable() 
    {
        GameEvents.gameStartSetUp += GameIsStarting;
        GameEvents.playerStartGame += GameStarted;
        GameEvents.gameEndSetUp += EndGameSetup;
    }

    private void OnDisable() 
    {
        GameEvents.gameStartSetUp -= GameIsStarting;
        GameEvents.playerStartGame -= GameStarted;
        GameEvents.gameEndSetUp -= EndGameSetup;
    }
    
    private void EndGameSetup(bool didPlayerWin)
    {
        if(didPlayerWin)
        {
            _waveComplete.SetActive(true);
        }
        else
        {   
            _playerLost.SetActive(true);
        }
    }
    
    private void GameIsStarting()
    {
        _mainWaveText.text = "Wave " + RoundData.Wave.ToString();
        _subWaveText.text = "Wave " + RoundData.Wave.ToString();
        playerHealth = FindObjectOfType<PlayerHealth>();
        _healthSlider.maxValue = playerHealth.maxHealth;
    }

    private void Update() 
    {
        _healthSlider.value = playerHealth.currentHealth;
    }

    private void GameStarted()
    {
        //_enemiesLeftText.text = InGameLevelManager.Instance._totalEnemiesCounter.ToString() + " Enemies left";
    }

    public void WhiteScreen()
    {
        _whiteScreenAnim.Play(FLASHWHITE);
    }

    public void EnemiesLeftUpdate(int enemiesLeft)
    {
        _enemiesLeftText.text = enemiesLeft.ToString() + " Enemies left";
    }

    public void UpdatePlayerHealth()
    {
        _healthSlider.maxValue = RoundData.PlayerMaxHealth;
    }

    public IEnumerator DisplayPoisonMessage()
    {
        yield return new WaitForSeconds(_waveInClip.length);
        _poisonText.SetActive(true);
    }
}
