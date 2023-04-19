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
    Transform _playerTransform;
    [Header("Enemy Movement")]
    [SerializeField] private float _enemyMovementSpeed;
    [SerializeField] private float _enemyMovementSpeedModifier;
    [SerializeField] private float _angleOffset;


    [Header("Bleed Out values")]
    [SerializeField] private Transform _explodePoint;
    private bool _alreadyDead;


    [Header("Archer Variables")]
    [SerializeField] private float _bowAttackRange;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private Transform _archerChild;
    [SerializeField] private Transform _arrowAttackPoint;
    [SerializeField] private Animator _enemyBowAnim;
    [SerializeField] private AnimationClip _EnemyBowAnimClip;
    const string SUBBOW = "Armature|Bow_Fire";
    const string BOWATTACK = "Archer_Attack";


    [Header("Animations")]
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private float _totalAttackCoolDown;
    [SerializeField] private float _attackCoolDown;
    const string SWORDATTACK = "Stable Sword Outward Slash";


    [Header("References")]
    [SerializeField] private EnemyWeaponBehaviour _enemyWeaponBehaviour;
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;
    [SerializeField] private AIPath _aIPath;

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
            if (Vector3.Distance(transform.position , _playerTransform.position) < _bowAttackRange)
            {
                if(_attackCoolDown <= 0)
                {
                    _enemyAnim.Play(BOWATTACK);
                    StartCoroutine(ShootTheBow());
                    _attackCoolDown = _totalAttackCoolDown;
                }
                _aIPath.canMove = false;
            }
            else
            {
                _aIPath.canMove = true;
            }
        }
        
        if(_enemyType == EnemyType.Sword || _enemyType == EnemyType.Hammer)
        {
            if(_aIPath.reachedEndOfPath)
            {
                if(_attackCoolDown <= 0)
                {
                    _enemyAnim.Play(SWORDATTACK);
                    _attackCoolDown = _totalAttackCoolDown;
                    StartCoroutine(_enemyWeaponBehaviour.Activate());
                }
                _aIPath.canMove = false;
            }
            else
            {
                _aIPath.canMove = true;
            }
        }
        
        if(_attackCoolDown > 0)
        {
            _attackCoolDown -= Time.deltaTime;
        }
        
        Vector3 playerPos = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
 
        transform.LookAt(playerPos);
    }

    IEnumerator ShootTheBow()
    {
        _enemyBowAnim.Play(SUBBOW);
        _archerChild.transform.eulerAngles = new Vector3(0,87,0);
        yield return new WaitForSeconds(_EnemyBowAnimClip.length - 0.2f);
        Rigidbody rb = Instantiate(_enemyWeaponBehaviour, _arrowAttackPoint.position, transform.rotation * Quaternion.Euler(0,180,0)).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * _arrowSpeed, ForceMode.Impulse);
        _archerChild.transform.eulerAngles = new Vector3(0,25,0);
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
