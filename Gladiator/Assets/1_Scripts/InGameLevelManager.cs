using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyList;






    public void EnemyHasDied()
    {
        if(_enemyList.Length <= 0)
        {
            GameEvents.gameEndSetUp?.Invoke();
        } 
    }
}
