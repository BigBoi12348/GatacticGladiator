using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    private EnemyBehaviour _enemyBehaviour;

    [Header("Function")]
    [SerializeField] private bool _doIKillInstantly;
    [SerializeField] private float _myBleedOutStrength;

    public void Init(EnemyBehaviour enemyBehaviour)
    {
        _enemyBehaviour = enemyBehaviour;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cap");
        // if(other.gameObject.TryGetComponent<>)
        // {
                _enemyBehaviour.BodyPartLost(_doIKillInstantly, _myBleedOutStrength);
                Destroy(gameObject);
        // }
    }
}
