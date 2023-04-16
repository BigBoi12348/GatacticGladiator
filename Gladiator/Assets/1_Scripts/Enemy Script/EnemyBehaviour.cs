using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBehaviour : MonoBehaviour
{   
    // [Header("Fracure Variables")]
    // public GameObject fractured;
    // public float breakforce;
    //[SerializeField] private Rigidbody _rb;
    Transform _playerTransform;
    [Header("Enemy Movement")]
    [SerializeField] private float _enemyMovementSpeed;
    [SerializeField] private float _enemyMovementSpeedModifier;
    

    [Header("Bleed Out values")]
    [SerializeField] private GameObject _deathSymbol;
    [SerializeField] private Transform _explodePoint;
    private bool _alreadyDead;


    [Header("Animations")]
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private float _totalAttackCoolDown;
    [SerializeField] private float _attackCoolDown;
    const string ATTACK = "Stable Sword Outward Slash";


    [Header("References")]
    [SerializeField] private EnemyWeaponBehaviour _enemyWeaponBehaviour;
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;
    [SerializeField] private AIPath _aIPath;
    [SerializeField] private Collider _groundDetectCollider;

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
        if(_aIPath.reachedEndOfPath)
        {
            if(_enemyAnim != null)
            {
                if(_attackCoolDown <= 0)
                {
                    _enemyAnim.Play(ATTACK);
                    _attackCoolDown = _totalAttackCoolDown;
                    StartCoroutine(_enemyWeaponBehaviour.Activate());
                }
            }
            else if(_playerTransform.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                if(_attackCoolDown <= 0)
                {
                    playerHealth.TakeDamage(5);
                    _attackCoolDown = _totalAttackCoolDown;  
                }
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

    private void Breaking()
    {
        PiecesHandler tempPiecesHandler = EnemyManager.Instance.GetAnDeadEnemy(transform);
        tempPiecesHandler.StartExplode(_explodePoint);
        //Destroy(gameObject);
    }

    private void Death()
    {
        Breaking();

        _alreadyDead = true;
        _aIPath.canMove = false;
        _aIDestinationSetter.enabled = false;
        _deathSymbol.SetActive(true);

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
