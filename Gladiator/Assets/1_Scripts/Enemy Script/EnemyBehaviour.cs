using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehaviour : MonoBehaviour
{   
    private enum EnemyType
    {
        Sword, Archer, Hammer, Axe
    }
    [SerializeField] private EnemyType _enemyType;
    [SerializeField]Transform _playerTransform;
    [Header("Enemy Movement")]
    [SerializeField] private float _enemyMovementSpeed;
    [SerializeField] private float _enemyMovementSpeedModifier;
    [SerializeField] private float _angleOffset;

    [Header("Bleed Out values")]
    [SerializeField] private Transform _explodePoint;
    private bool _alreadyDead;
    [Header("Archer Variables")]
    [SerializeField] private bool _playerInAttackRange;

    [Header("Animations")]
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private float _totalAttackCoolDown;
    [SerializeField] private float _attackCoolDown;
    const string SWORDATTACK = "Stable Sword Outward Slash";


    [Header("References")]
    [SerializeField] private EnemyWeaponBehaviour _enemyWeaponBehaviour;
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;
    [SerializeField] private AIPath _aIPath;
    //[SerializeField] private Collider _groundDetectCollider;

    private void Awake() 
    {

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
        if(_enemyType == EnemyType.Archer)
        {
            // if (Vector3.Distance(transform.position, _playerTransform.position) < 20)
            // {
            //     _playerInSightRange = true;
            // }
            // else
            // {
            //     _playerInSightRange = false;
            // }
            if (Vector3.Distance(transform.position , _playerTransform.position) < 20)
            {
                _playerInAttackRange = true;
            }
            else
            {
                _playerInAttackRange = false;
            }
        }
        
        if(_aIPath.reachedEndOfPath)
        {
            if(_attackCoolDown <= 0)
            {
                _enemyAnim.Play(SWORDATTACK);
                _attackCoolDown = _totalAttackCoolDown;
                StartCoroutine(_enemyWeaponBehaviour.Activate());
            }
        }
        
        if(_attackCoolDown > 0)
        {
            _attackCoolDown -= Time.deltaTime;
        }
        
        Vector3 playerPos = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
 
        transform.LookAt(playerPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon"))
        {
            Death();
        }
    }

    public void StopEnemy()
    {
        _enemyAnim.enabled = false;
        _aIPath.enabled = false;
    }

    private void Breaking()
    {
        PiecesHandler tempPiecesHandler = EnemyManager.Instance.GetAnDeadEnemy(transform);
        tempPiecesHandler.StartExplode(_explodePoint);
    }

    public void Death()
    {
        Breaking();

        _alreadyDead = true;
        _aIPath.canMove = false;
        _aIDestinationSetter.enabled = false;

        if(_enemyWeaponBehaviour != null)
        {
            _enemyWeaponBehaviour.enabled = false;
        }
        if(_enemyAnim != null)
        {
            _enemyAnim.enabled = false;
        }
        InGameLevelManager.Instance.EnemyHasDied();
        SoundManager.Instance.PlaySound3D(SoundManager.Sound.EnemyDeath, transform.position);
        Destroy(gameObject);
    }
}
