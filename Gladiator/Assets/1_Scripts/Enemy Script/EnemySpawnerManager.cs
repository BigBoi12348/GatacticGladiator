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
                    SpawnEnemy(_enemiesToSpawn[0], _spawnLocations[Random.Range(0,_spawnLocations.Length)].position);
                    _enemiesToSpawn.RemoveAt(0);
                    _spawnTimer = _spawnInterval;
                }
                else
                {

                }
            }
            else
            {
                _spawnTimer -= Time.deltaTime;
            }
        }
    }
    
    private void GameIsStarting()
    {
        if(RoundData.DifficultyRank > 0)
        {
            _currentDifficultyRank = RoundData.DifficultyRank;
        }
        else
        {
            _currentDifficultyRank = 5;
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
            int randNum = Random.Range(0,tempEnemyToChoseFrom.Count);

            GameObject tempEnemyObj = tempEnemyToChoseFrom[randNum].EnemyObj;
            tempDifficultyScore -= tempEnemyToChoseFrom[randNum].DifficultyRank;
            _enemiesToSpawn.Add(tempEnemyObj);
            TotalEnemiesSpawningThisRound++;
        }

        RoundData.DifficultyRank = (int)Mathf.Pow(_currentDifficultyRank, 1.2f);
        GameEvents.playerStartGame?.Invoke();
    }

    private void PlayerHasStartedGame()
    {
        _canSpawn = true;
    }

    private void GameEndSetUp(bool cum)
    {
        
    }

    private void EndOfRound()
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
