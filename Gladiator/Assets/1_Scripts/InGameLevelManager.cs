using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : MonoBehaviour
{   
    public static InGameLevelManager Instance;

    [Header(":sa")]
    private int _totalEnemiesCounter;
    public int CurrentRound{get; private set;}


    [Header("References")]
    [SerializeField] private EnemySpawnerManager _enemySpawnerManager;

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
        CurrentRound = 1;
    }

    private void Start() 
    {
        GameEvents.gameStartSetUp?.Invoke();
    }

    private void OnEnable() 
    {
        GameEvents.gameStartSetUp += GameIsStarting;
        GameEvents.gameEndSetUp += GameEndSetUp;
        GameEvents.playerFinsihedGame += EndOfRound;
    }

    private void OnDisable() 
    {
        GameEvents.gameStartSetUp -= GameIsStarting;
        GameEvents.gameEndSetUp -= GameEndSetUp;
        GameEvents.playerFinsihedGame -= EndOfRound;
    }

    private void GameIsStarting()
    {
        _totalEnemiesCounter = _enemySpawnerManager.TotalEnemiesSpawningThisRound;
    }

    private void GameEndSetUp()
    {

    }

    private void EndOfRound()
    {
        
    }
    
    public void EnemyHasDied()
    {
        _totalEnemiesCounter--;
        if(_totalEnemiesCounter <= 0)
        {
            Debug.Log("GameEndSetUp");
            GameEvents.gameEndSetUp?.Invoke();
        }
    }
}