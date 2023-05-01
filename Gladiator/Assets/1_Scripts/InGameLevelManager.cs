using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : MonoBehaviour
{   
    public static InGameLevelManager Instance;
    private UIManager _uIManager;

    public int _totalEnemiesCounter{get; private set;}
    public int CurrentRound{get; private set;}
    public int BonusCredits;

    [Header("Environments")]
    [SerializeField] private GameObject _plainLevel;
    [SerializeField] private GameObject _fireLevel;
    [SerializeField] private GameObject _poisonLevel;


    [Header("References")]
    [SerializeField] private EnemySpawnerManager _enemySpawnerManager;
    [SerializeField] private AnimationClip _waveComplete;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerShieldBehaviour _playerShieldBehaviour;
    [SerializeField] private KillComboHandler killComboHandler;
    [SerializeField] private FirstPersonController _firstPersonController;

    private bool healOnKill;
    private bool _permanentSpeedOn;
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

        _uIManager = FindObjectOfType<UIManager>();
    }

    private void Start() 
    {
        GameManager.Instance.UnFreezeGame();
        GameEvents.gameStartSetUp?.Invoke();

        if(PlayerUpgradesData.AttackThree)
        {
            healOnKill = true;
        }
        else
        {
            healOnKill = false;
        }

        if(PlayerUpgradesData.AttackTwo)
        {
            _permanentSpeedOn = true;
        }
        else
        {
            _permanentSpeedOn = false;
        }
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
        
        //Sets the level
        _plainLevel.SetActive(false);
        _fireLevel.SetActive(false);
        _poisonLevel.SetActive(false);

        if(CurrentRound == 1 || CurrentRound == 4)
        {
           _plainLevel.SetActive(true); 
        }
        else if(CurrentRound == 2 || CurrentRound == 5)
        {
            _fireLevel.SetActive(true);
        }
        else if(CurrentRound == 3 || CurrentRound == 6)
        {
            _poisonLevel.SetActive(true);
        }
        else if(CurrentRound >= 7)
        {
            if(CurrentRound % 2 == 0)
            {
                _poisonLevel.SetActive(true);
            }
            else
            {
                _fireLevel.SetActive(true);
            }
        }
    }

    private void GameStarted()
    {
        _totalEnemiesCounter = _enemySpawnerManager.TotalEnemiesSpawningThisRound;
        _uIManager.EnemiesLeftUpdate(_totalEnemiesCounter);
    }

    private void GameEndSetUp(bool didPlayerWin)
    {
        Time.timeScale = 1f;
        if(didPlayerWin)
        {
            RoundData.Wave++;
            RoundData.PlayerPoints += 8;
            PlayerRoundStats.CreditsEarned += 8;
            PlayerRoundStats.HighestWaveReached = RoundData.Wave;
        }
        else if(!didPlayerWin)
        {
            RoundData.Wave = 1;
            RoundData.PlayerPoints = 0;
            RoundData.DifficultyRank = 0;
            RoundData.PlayerMaxHealth = 60;
            ResetPlayerUpgradeData();
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
            GameManager.Instance.LoadThisScene(GameManager.MAINMENUSCENE);
        }
    }

    private void EndOfRound()
    {
        GameManager.Instance.LoadThisScene(GameManager.UPGRADESCENE);
    }
    
    public void EnemyHasDied()
    {
        if(healOnKill)
        {
            if(KillComboHandler.KillComboCounter >= 50)
            {
                RoundData.PlayerMaxHealth++;
                _uIManager.UpdatePlayerHealth();
            }
            _playerHealth.AddHealth(1);
        }


        if(PlayerUpgradesData.ShieldOne)
        {
            if(KillComboHandler.KillComboCounter >= 55)
            {
                int chance = Random.Range(1,101);
                if(chance <= 25)
                {
                    _playerShieldBehaviour.AddRecharge(5);
                }
            }
        }

        if(PlayerUpgradesData.StarFive)
        {
            if(KillComboHandler.KillComboCounter >= 60)
            {
                int chance = Random.Range(1,101);
                if(chance <= 10)
                {
                    _firstPersonController.CoolDownReduce(1);
                } 
            }
        }

        if(_permanentSpeedOn)
        {
            if(KillComboHandler.KillComboCounter >= 65)
            {
                int chance = Random.Range(1,101);
                if(chance <= 15)
                {
                    killComboHandler.GivePlayerAttackSpeed(0.05f);
                }
            }
        }

        _totalEnemiesCounter--;
        PlayerRoundStats.TotalEnemiesKilled++;
        _uIManager.EnemiesLeftUpdate(_totalEnemiesCounter);
        killComboHandler.AddToCombo(1);

        if(_totalEnemiesCounter == 0)
        {
            GameEvents.gameEndSetUp?.Invoke(true);
        }
    }

    public void FlashScreenWhite()
    {
        _uIManager.WhiteScreen();
    }

    private void ResetPlayerUpgradeData()
    {
        PlayerUpgradesData.AttackOne = false;
        PlayerUpgradesData.AttackTwo = false;
        PlayerUpgradesData.AttackThree = false;
        PlayerUpgradesData.AttackFour = false;
        PlayerUpgradesData.AttackFive = false;
        PlayerUpgradesData.ShieldOne = false;
        PlayerUpgradesData.ShieldTwo = false;
        PlayerUpgradesData.ShieldThree = false;
        PlayerUpgradesData.ShieldFour = false;
        PlayerUpgradesData.ShieldFive = false;
        PlayerUpgradesData.StarOne = false;
        PlayerUpgradesData.StarTwo = false;
        PlayerUpgradesData.StarThree = false;
        PlayerUpgradesData.StarFour = false;
        PlayerUpgradesData.StarFive = false;
    }
}
