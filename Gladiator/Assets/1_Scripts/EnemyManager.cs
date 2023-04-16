using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Transform _destoryedEnemyContainer;
    [SerializeField] private Transform _hidePoolObjects;
    [SerializeField] private GameObject _destoryedEnemyState;
    private ObjectPool<GameObject> _deadEnemyStatePool;
    private Transform _currentAskingObj;
    private void Awake()
    {
        Instance = this;

        _deadEnemyStatePool = new ObjectPool<GameObject>(() => 
        {
            return Instantiate(_destoryedEnemyState, _hidePoolObjects.position, Quaternion.identity, _hidePoolObjects);
        }, deadEnemy => {
            deadEnemy.transform.position = _currentAskingObj.position;
            deadEnemy.transform.rotation = _currentAskingObj.rotation;
            deadEnemy.transform.parent = _destoryedEnemyContainer;
        }, deadEnemy => {
            deadEnemy.transform.position = _hidePoolObjects.position;
            deadEnemy.transform.parent = _hidePoolObjects;
        }, deadEnemy => {
            Destroy(deadEnemy);
        }, true, 150, 500);

        PreLoad();
    }

    private void PreLoad()
    {
        _currentAskingObj = _hidePoolObjects;

        for (int i = 0; i < 150; i++)
        {
            _deadEnemyStatePool.Get(); 
        }
    }

    private void Start() 
    {
        _deadEnemyStatePool.Get();
    }

    private void Update() 
    {
        
    }

    public PiecesHandler GetAnDeadEnemy(Transform askingObj)
    {
        _currentAskingObj = askingObj;
        GameObject deadEnemy = _deadEnemyStatePool.Get();
        PiecesHandler tempPi = deadEnemy.GetComponent<PiecesHandler>();
        tempPi.Init(this);
        return tempPi;
    }

    public void ReturnDeadEnemy(PiecesHandler piecesHandler)
    {
        
    }
}
