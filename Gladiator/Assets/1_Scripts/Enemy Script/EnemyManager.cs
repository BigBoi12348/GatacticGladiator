using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Transform _destoryedEnemyContainer;
    [SerializeField] private Transform _hidePoolObjects;
    [SerializeField] private PiecesHandler _241_destoryedEnemyState;
    [SerializeField] private PiecesHandler _53_destoryedEnemyState;
    private ObjectPool<PiecesHandler> _241_deadEnemyStatePool;
    private ObjectPool<PiecesHandler> _53_deadEnemyStatePool;
    private Transform _currentAskingObj;
    private int optimisedCounter;
    private void Awake()
    {
        Instance = this;

        _241_deadEnemyStatePool = new ObjectPool<PiecesHandler>(() => 
        {
            return Instantiate(_241_destoryedEnemyState, _hidePoolObjects.position, Quaternion.identity, _hidePoolObjects);
        }, deadEnemy => {
            optimisedCounter++;
            deadEnemy.transform.position = _currentAskingObj.position;
            deadEnemy.transform.rotation = _currentAskingObj.rotation;
            deadEnemy.transform.parent = _destoryedEnemyContainer;
        }, deadEnemy => {
            optimisedCounter--;
            deadEnemy.gameObject.SetActive(false);
            deadEnemy.transform.position = _hidePoolObjects.position;
            deadEnemy.transform.parent = _hidePoolObjects;
        }, deadEnemy => {
            Destroy(deadEnemy);
        }, true, 20, 30);

        _53_deadEnemyStatePool = new ObjectPool<PiecesHandler>(() => 
        {
            return Instantiate(_53_destoryedEnemyState, _hidePoolObjects.position, Quaternion.identity, _hidePoolObjects);
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
        for (int i = 0; i < 15; i++)
        {
            piecesHandlers.Add(_241_deadEnemyStatePool.Get()); 
        }

        for (int i = 0; i < 15; i++)
        {
            _241_deadEnemyStatePool.Release(piecesHandlers[i]);
        }

        piecesHandlers = new List<PiecesHandler>();
        for (int i = 0; i < 80; i++)
        {
            piecesHandlers.Add(_53_deadEnemyStatePool.Get()); 
        }

        for (int i = 0; i < 80; i++)
        {
            _53_deadEnemyStatePool.Release(piecesHandlers[i]);
        }
    }

    public PiecesHandler GetAnDeadEnemy(Transform askingObj)
    {
        _currentAskingObj = askingObj;
        PiecesHandler deadEnemy;
        if(optimisedCounter <= 15)
        {
            deadEnemy = _241_deadEnemyStatePool.Get();
            deadEnemy.Init(this, true, false);
        }
        else
        {
            deadEnemy = _53_deadEnemyStatePool.Get();
            if(optimisedCounter < 50)
            {
                deadEnemy.Init(this, false, false);
            }
            else
            {
                deadEnemy.Init(this, false, true);
            }
        }
        deadEnemy.gameObject.SetActive(true);
        return deadEnemy;
    }

    public void ReturnDeadEnemy(PiecesHandler piecesHandler, bool type)
    {
        if(type)
        {
            _241_deadEnemyStatePool.Release(piecesHandler);
        }
        else 
        {
            _241_deadEnemyStatePool.Release(piecesHandler);
        }
    }
}
