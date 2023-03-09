using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehaviour : MonoBehaviour
{   
    Transform _playerTransform;
    [Header("Enemy Movement")]
    [SerializeField] private float _enemyMovementSpeed;
    [SerializeField] private float _enemyMovementSpeedModifier;
    

    [Header("Bleed Out values")]
    private bool _bodyPartMissing;
    [SerializeField] private float _totalBleedOut;
    [SerializeField] private float _bleedOutStrength;
    [SerializeField] private float _bleedOutModifier;
    private bool _alreadyDead;
    

    [Header("Body Parts")]
    [SerializeField] private EnemyBodyPart[] enemyBodyParts;


    [Header("References")]
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;
    [SerializeField] private AIPath _aIPath;
    [SerializeField] private Collider _groundDetectCollider;

    private void Awake() 
    {
        foreach (var enemyBodyPart in enemyBodyParts)
        {
            enemyBodyPart.Init(this);
        }
    }

    public void Init(Transform playerTarget)
    {
        _playerTransform = playerTarget;
        _aIDestinationSetter.target = playerTarget;
    }
    
    private void Start() 
    {
        _aIPath.maxSpeed = _enemyMovementSpeed;
    }

    private void Update()
    {
        if(_bodyPartMissing)
        {
            if(_totalBleedOut > 0)
            {
                _totalBleedOut -= _bleedOutStrength + _bleedOutModifier;
            }
            else
            {
                Death();
            }
        }
        
        transform.LookAt(_playerTransform);
        //Fix Enemy Rotation
        // float angle = Vector3.Angle(transform.position, _playerTransform.position);
        // Debug.Log(angle);
        // transform.localEulerAngles = new Vector3(0,angle,0);
    }


    public void BodyPartLost(bool doIDie, float bleedingStrength)
    {
        if(!_alreadyDead)
        {
            if(doIDie)
            {
                Death();
            }
            else
            {
                _bodyPartMissing = true;
                _bleedOutModifier += bleedingStrength;
            }
        }
    }

    private void Death()
    {
        _alreadyDead = true;
        _aIPath.canMove = false;
        _aIDestinationSetter.enabled = false;
        InGameLevelManager.Instance.EnemyHasDied();
        Destroy(this);
    }
}
