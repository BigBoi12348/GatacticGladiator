using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    [Header("Enemy Spawn Control")]
    [SerializeField] private bool _canSpawn;
    [SerializeField] private float _spawnInterval;
    private float _spawnTimer;
    [SerializeField] private int _currentDifficultyRank;


    [Header("Enemy Control")]
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Transform[] _spawnLocations;
    [SerializeField] private Transform[] _playerCloseSpawnLocations;
    public int TotalEnemiesSpawningThisRound{get; private set;}
    [SerializeField]private bool _spawnEnemies;
    [SerializeField]private int _spawnEnemyCount;
    [SerializeField]private bool _isUsingWaves;
    [SerializeField]private bool _isWaitingForDeath;
    [SerializeField]private float _isUsingWavesTimer;
    [SerializeField]private List<Transform> _currentEnemiesThisWave;
    

    [Header("Enemies")]
    [SerializeField] private Enemy[] enemyTypes;
    private List<GameObject> _enemiesToSpawn = new List<GameObject>();
    private List<EnemyBehaviour> _enemyBehaviours = new List<EnemyBehaviour>();

    private void OnEnable() 
    {
        GameEvents.gameStartSetUp += GameIsStarting;
        GameEvents.playerStartGame += PlayerHasStartedGame;
        GameEvents.gameEndSetUp += GameEndSetUp;
        GameEvents.playerFinsihedGame += EndOfRound;
    }

    private void OnDisable() 
    {
        GameEvents.gameStartSetUp -= GameIsStarting;
        GameEvents.playerStartGame -= PlayerHasStartedGame;
        GameEvents.gameEndSetUp -= GameEndSetUp;
        GameEvents.playerFinsihedGame -= EndOfRound;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(_canSpawn)
        {
            if(_spawnTimer <= 0)
            {
                if(_enemiesToSpawn.Count > 0)
                {
                    if(_isUsingWaves)
                    {
                        if(!_isWaitingForDeath)
                        {
                            if(_spawnEnemies)
                            {
                                SpawnAEnemy();
                                _spawnEnemyCount--;
                            }
                            if(_isUsingWavesTimer < 0)
                            {
                                NewWave();
                            }
                            _isUsingWavesTimer -= Time.deltaTime;
                        }
                        else
                        {
                            if(_spawnEnemies)
                            {
                                for (int i = 0; i < _spawnEnemyCount; i++)
                                {
                                    SpawnAEnemy();
                                }
                                _currentEnemiesThisWave = new List<Transform>();
                                foreach (Transform enemy in _enemyContainer)
                                {
                                    if(enemy.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
                                    {
                                        _currentEnemiesThisWave.Add(enemy);
                                    }
                                }
                                _spawnEnemyCount = 0;
                                _spawnEnemies = false;
                            }
                            for (int i = 0; i < _currentEnemiesThisWave.Count; i++)
                            {
                                if(_currentEnemiesThisWave[i] != null)
                                {
                                    break;
                                }
                                else if(_enemyContainer.childCount > 1)
                                {                                                   
                                    break;                                    
                                }
                                NewWave();
                            }
                        }
                    }
                    else
                    {
                        SpawnAEnemy();
                        int chance = Random.Range(1,101);
                        if(chance < 5)
                        {
                            NewWave();
                        }
                    }
                    if(_spawnEnemyCount <= 0)
                    {
                        _spawnEnemies = false;
                    }
                }
            }
            else
            {
                _spawnTimer -= Time.deltaTime;
            }
        }
    }

    private void NewWave()
    {
        
        int usingWaveChance = Random.Range(1,101); 
        
        if(usingWaveChance < 60)
        {
            _isUsingWaves = true;
            int usingOnDeathChance = Random.Range(1,101); 
            if(usingOnDeathChance < 60)
            {
                _isWaitingForDeath = true;
            }
            else
            {
                _isWaitingForDeath = false;
                _isUsingWavesTimer = Random.Range(5,10);
            }
        }

        if(RoundData.DifficultyRank/10 < 40)
        {
            _spawnEnemyCount = Random.Range(RoundData.DifficultyRank/5, RoundData.DifficultyRank/10);
        }
        else
        {
            _spawnEnemyCount = Random.Range(25, 40);
        }
        
        if(_spawnEnemyCount > _enemiesToSpawn.Count)
        {
            _spawnEnemyCount = _enemiesToSpawn.Count;
        }
        _spawnEnemies = true;
    }

    private void SpawnAEnemy()
    {
        SpawnEnemy(_enemiesToSpawn[0], _spawnLocations[Random.Range(0, _spawnLocations.Length)].position);
        _enemiesToSpawn.RemoveAt(0);
        _spawnTimer = _spawnInterval;
    }
    
    private void GameIsStarting()
    {
        if(RoundData.DifficultyRank > 0)
        {
            _currentDifficultyRank = RoundData.DifficultyRank;
        }
        else
        {
            _currentDifficultyRank = 15;
        }

        int tempDifficultyScore = _currentDifficultyRank;
        int tempCurrentRound = InGameLevelManager.Instance.CurrentRound;
        _enemiesToSpawn = new List<GameObject>();
        
        while (tempDifficultyScore > 0)
        {
            List<Enemy> tempEnemyToChoseFrom = new List<Enemy>();
            foreach (var enemy in enemyTypes)
            {
                if(tempCurrentRound >= enemy.StartSpawnWave)
                {
                    if(enemy.IsThereStopForThisEnemy)
                    {
                        if(tempCurrentRound <= enemy.StopSpawnWave)
                        {
                            tempEnemyToChoseFrom.Add(enemy);
                        }
                    }
                    else
                    {
                        tempEnemyToChoseFrom.Add(enemy);
                    }
                }
            }
            int randNum = Random.Range(0, tempEnemyToChoseFrom.Count);

            GameObject tempEnemyObj = tempEnemyToChoseFrom[randNum].EnemyObj;
            tempDifficultyScore -= tempEnemyToChoseFrom[randNum].DifficultyRank;
            _enemiesToSpawn.Add(tempEnemyObj);
            TotalEnemiesSpawningThisRound++;
        }

        RoundData.DifficultyRank = RoundData.DifficultyRank + 15;
        // (int)Mathf.Pow(_currentDifficultyRank, 1.1f);
        int chance = Random.Range(1,101);
        if(chance < 60)
        {
            _isUsingWaves = true;
        }
        GameEvents.playerStartGame?.Invoke();
    }

    private void PlayerHasStartedGame()
    {
        NewWave();
        _canSpawn = true;
    }

    private void GameEndSetUp(bool cum)
    {
        
    }

    private void EndOfRound(bool state)
    {
        foreach (Transform enemyTransform in _enemyContainer)
        {
            Destroy(enemyTransform.gameObject);
        }
    }

    private void SpawnEnemy(GameObject enemyToSpawn, Vector3 locationToSpawn)
    {
        GameObject tempEnemyObj = Instantiate(enemyToSpawn, locationToSpawn, Quaternion.identity, _enemyContainer);
        EnemyBehaviour tempEnemyBehaviour = tempEnemyObj.GetComponent<EnemyBehaviour>();
        tempEnemyBehaviour.Init(_playerTransform);
        _enemyBehaviours.Add(tempEnemyBehaviour);
    }
}


[System.Serializable]
public class Enemy
{
    public GameObject EnemyObj;
    public int DifficultyRank;
    [Tooltip("Limit when they can spawn until the wave coutner reaches it")]
    public int StartSpawnWave;
    public bool IsThereStopForThisEnemy;
    public int StopSpawnWave;
}
