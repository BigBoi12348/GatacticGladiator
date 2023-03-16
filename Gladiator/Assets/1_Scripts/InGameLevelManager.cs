using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : MonoBehaviour
{   
    public static InGameLevelManager Instance;
    private UIManager uIManager;

    public int _totalEnemiesCounter{get; private set;}
    public int CurrentRound{get; private set;}


    [Header("References")]
    [SerializeField] private EnemySpawnerManager _enemySpawnerManager;
    [SerializeField] private AnimationClip _waveComplete;

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

        uIManager = FindObjectOfType<UIManager>();
    }

    private void Start() 
    {
        GameEvents.gameStartSetUp?.Invoke();
    }

    private void OnEnable() 
    {
        GameEvents.gameStartSetUp += GameIsStarting;
        GameEvents.playerStartGame += GameStarted;
        GameEvents.gameEndSetUp += GameEndSetUp;
        GameEvents.playerFinsihedGame += EndOfRound;
    }

    private void OnDisable() 
    {
        GameEvents.gameStartSetUp -= GameIsStarting;
        GameEvents.gameEndSetUp -= GameEndSetUp;
        GameEvents.playerStartGame -= GameStarted;
        GameEvents.playerFinsihedGame -= EndOfRound;
    }

    private void GameIsStarting()
    {
        CurrentRound = RoundData.Wave;
        //_totalEnemiesCounter = _enemySpawnerManager.TotalEnemiesSpawningThisRound;
    }

    private void GameStarted()
    {
        _totalEnemiesCounter = _enemySpawnerManager.TotalEnemiesSpawningThisRound;
        uIManager.EnemiesLeftUpdate(_totalEnemiesCounter);
    }

    private void GameEndSetUp()
    {
        RoundData.Wave++;
        RoundData.PlayerPoints += 3;
        StartCoroutine(delayFinishGame());
    }
    IEnumerator delayFinishGame()
    {
        yield return new WaitForSecondsRealtime(_waveComplete.length);
        GameEvents.playerFinsihedGame?.Invoke();
    }
    private void EndOfRound()
    {
        GameManager.Instance.LoadThisScene(2);
    }
    
    public void EnemyHasDied()
    {
        Debug.Log("Enemy died");
        _totalEnemiesCounter--;
        uIManager.EnemiesLeftUpdate(_totalEnemiesCounter);

        if(_totalEnemiesCounter == 0)
        {
            GameEvents.gameEndSetUp?.Invoke();
        }
    }
}
