using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Transform _destoryedEnemyContainer;
    [SerializeField] private Transform _hidePoolObjects;
    [SerializeField] private PiecesHandler _destoryedEnemyState;
    private ObjectPool<PiecesHandler> _deadEnemyStatePool;
    private Transform _currentAskingObj;
    
    private void Awake()
    {
        Instance = this;

        _deadEnemyStatePool = new ObjectPool<PiecesHandler>(() => 
        {
            return Instantiate(_destoryedEnemyState, _hidePoolObjects.position, Quaternion.identity, _hidePoolObjects);
        }, deadEnemy => {
            deadEnemy.transform.position = _currentAskingObj.position;
            deadEnemy.transform.rotation = _currentAskingObj.rotation;
            deadEnemy.transform.parent = _destoryedEnemyContainer;
        }, deadEnemy => {
            deadEnemy.gameObject.SetActive(false);
            deadEnemy.transform.position = _hidePoolObjects.position;
            deadEnemy.transform.parent = _hidePoolObjects;
        }, deadEnemy => {
            Destroy(deadEnemy);
        }, true, 101, 500);

        PreLoad();
    }

    private void PreLoad()
    {
        _currentAskingObj = _hidePoolObjects;
        List<PiecesHandler> piecesHandlers = new List<PiecesHandler>();
        for (int i = 0; i < 100; i++)
        {
            piecesHandlers.Add(_deadEnemyStatePool.Get()); 
        }

        for (int i = 0; i < 100; i++)
        {
            _deadEnemyStatePool.Release(piecesHandlers[i]);
        }
    }

    private void Start() 
    {
        //_deadEnemyStatePool.Get();
    }

    private void Update() 
    {
        
    }

    public PiecesHandler GetAnDeadEnemy(Transform askingObj)
    {
        _currentAskingObj = askingObj;
        PiecesHandler deadEnemy = _deadEnemyStatePool.Get();
        deadEnemy.gameObject.SetActive(true);
        deadEnemy.Init(this);
        return deadEnemy;
    }

    public void ReturnDeadEnemy(PiecesHandler piecesHandler)
    {
        _deadEnemyStatePool.Release(piecesHandler);
    }
}
