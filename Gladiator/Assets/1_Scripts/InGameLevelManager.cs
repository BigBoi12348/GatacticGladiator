using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : MonoBehaviour
{   
    public static InGameLevelManager Instance;
    private UIManager uIManager;

    public int _totalEnemiesCounter{get; private set;}
    public int CurrentRound{get; private set;}
    public GameObject[] levels;


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
        GameManager.Instance.UnFreezeGame();
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
        for(int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        CurrentRound = RoundData.Wave;
        levels[Random.Range(0, 3)].SetActive(true);
    }

    private void GameStarted()
    {
        _totalEnemiesCounter = _enemySpawnerManager.TotalEnemiesSpawningThisRound;
        uIManager.EnemiesLeftUpdate(_totalEnemiesCounter);
    }

    private void GameEndSetUp(bool didPlayerWin)
    {
        if(didPlayerWin)
        {
            RoundData.Wave++;
            RoundData.PlayerPoints += 3;
            //StartCoroutine(delayFinishGame(didPlayerWin));
        }
        else if(!didPlayerWin)
        {
            RoundData.Wave = 1;
            RoundData.PlayerPoints = 0;
            RoundData.DifficultyRank = 0;
            PlayerUpgradesData.AttackAttribute = 0;
            PlayerUpgradesData.ShieldAttribute = 0;
            PlayerUpgradesData.AbilityAttribute = 0;
        }
        StartCoroutine(delayFinishGame(didPlayerWin));
    }

    IEnumerator delayFinishGame(bool didPlayerWin)
    {
        if(!didPlayerWin)
        {
            GameManager.Instance.FreezeGame();
        }

        yield return new WaitForSecondsRealtime(_waveComplete.length);

        if(didPlayerWin)
        {
            GameEvents.playerFinsihedGame?.Invoke();
        }
        else if(!didPlayerWin)
        {
            GameManager.Instance.LoadThisScene(0);
        }
    }

    private void EndOfRound()
    {
        GameManager.Instance.LoadThisScene(2);
    }
    
    public void EnemyHasDied()
    {
        _totalEnemiesCounter--;
        uIManager.EnemiesLeftUpdate(_totalEnemiesCounter);

        if(_totalEnemiesCounter == 0)
        {
            GameEvents.gameEndSetUp?.Invoke(true);
        }
    }
}
