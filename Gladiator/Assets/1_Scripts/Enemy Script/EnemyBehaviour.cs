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
    Rigidbody myRb;

    [Header("Enemy Movement")]
    [SerializeField] private float _enemyWalkSpeed;
    [SerializeField] private float _enemyRunSpeed;
    [SerializeField] private float _distanceAwayUntilWalk;
    [SerializeField] private float _totalAttackCoolDown;
    [SerializeField] private float _attackCoolDown;
    [Range(0.2f, 2.3f)]
    [SerializeField] private float _enemyAnimSpeedModifier;
    [SerializeField] private float _angleOffset;

    [Header("Misc")]
    [SerializeField] private GameObject _meshColliderFollow;
    [SerializeField] private Transform _explodePoint;
    private float _moveSoundIntervalTimer = 0.7f;
    private float lastTimePlayed;
    bool changedColType;

    [Header("Archer Variables")]
    [SerializeField] private ShootArrowBehaviour _shootArrowBehaviour;
    [SerializeField] private float _bowAttackRange;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private Transform _archerChild;
    [SerializeField] private Transform _arrowAttackPoint;
    public bool _canMoveAgain;
    [SerializeField] private Animator _enemyBowAnim;
    [SerializeField] private AnimationClip _EnemyBowAnimClip;
    const string SUBBOW = "Armature|Bow_Fire";
    const string BOWATTACK = "Archer_Attack";


    [Header("Animations")]
    [SerializeField] private Animator _enemyAnim;
    [SerializeField] private AnimationClip _attackAnimClip;
    
    const string SWORDATTACK = "Stable Sword Outward Slash";


    [Header("References")]
    [SerializeField] private EnemyWeaponBehaviour _enemyWeaponBehaviour;
    [SerializeField] private WallStopperBehaviour _wallStopper;
    [SerializeField] private Collider _normalCol;
    [SerializeField] private AIDestinationSetter _aIDestinationSetter;
    [SerializeField] private AIPath _aIPath;
    bool alreadyDying;

    bool locked;
    bool hasBetterDash;
    bool explodePosChange;
    private void Awake() 
    {
        alreadyDying = false;
        myRb = GetComponent<Rigidbody>();
    }

    public void Init(Transform playerTarget)
    {
        _playerTransform = playerTarget;
        _aIDestinationSetter.target = playerTarget;
    }
    
    private void Start() 
    {
        _canMoveAgain = false;
        _aIPath.maxSpeed = _enemyWalkSpeed;
        if(PlayerUpgradesData.StarOne)
        {
            hasBetterDash = true;
        }
        else
        {
            hasBetterDash = false;
        }
        _canMoveAgain = true;
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
                    StartCoroutine(IncreaseAnimSpeed());
                    StartCoroutine(ShootTheBow());
                    _attackCoolDown = _totalAttackCoolDown;
                }
                if(changedColType)
                {
                    //_meshColliderFollow.SetActive(true);
                    //_normalCol.enabled = false;
                    changedColType = false;
                }
                _aIPath.canMove = false;
            }
            else 
            {
                if(!changedColType)
                {
                    //_meshColliderFollow.SetActive(false);
                    //_normalCol.enabled = true;
                    changedColType = true;
                }
                if(_canMoveAgain)
                {
                    _aIPath.canMove = true;
                }
            }
        }
        
        if(_enemyType == EnemyType.Sword || _enemyType == EnemyType.Hammer)
        {
            if(_aIPath.remainingDistance > _distanceAwayUntilWalk)
            {
                _enemyAnim.SetBool("IsRunning", true);
                _aIPath.maxSpeed = _enemyRunSpeed;
            }
            else
            {
                _enemyAnim.SetBool("IsRunning", false);
                _aIPath.maxSpeed = _enemyWalkSpeed;
                if(lastTimePlayed + _moveSoundIntervalTimer < Time.time)
                {
                    SoundManager.Instance.PlaySound3D(SoundManager.Sound.EnemyStep, transform.position);
                    lastTimePlayed = Time.time;
                }
            }

            if(_aIPath.reachedEndOfPath)
            {
                if(_attackCoolDown <= 0)
                {
                    _enemyAnim.Play(SWORDATTACK);
                    _attackCoolDown = _totalAttackCoolDown;
                    StartCoroutine(IncreaseAnimSpeed());
                    StartCoroutine(_enemyWeaponBehaviour.Activate());
                    _aIPath.canMove = false;
                }
            }
            else
            {
                if(_enemyType == EnemyType.Hammer)
                {
                    if(_canMoveAgain)
                    {
                        _aIPath.canMove = true;
                    }
                    else
                    {
                        _aIPath.canMove = false;
                    }
                }
                else
                {
                    _aIPath.canMove = true;
                }
            }
        }

        if(_attackCoolDown > 0)
        {
            _attackCoolDown -= Time.deltaTime;
        }
        
        Vector3 playerPos = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);

        if(!locked)
        {
            transform.LookAt(playerPos);
        }
    }
    
    public void CantMove()
    {
        _canMoveAgain = false;
    }

    public void CanMoveAgain()
    {
        _canMoveAgain = true;
    }

    IEnumerator IncreaseAnimSpeed()
    {
        _enemyAnim.speed = _enemyAnimSpeedModifier;
        if(_enemyType == EnemyType.Archer)
        {
            _enemyBowAnim.speed = _enemyAnimSpeedModifier;
        }
        yield return new WaitForSeconds(_attackAnimClip.length);
        _enemyAnim.speed = 1;
        if(_enemyType == EnemyType.Archer)
        {
            _enemyBowAnim.speed = 1;
        }
    }

    IEnumerator ShootTheBow()
    {
        _canMoveAgain = false;
        _shootArrowBehaviour.Init(_arrowSpeed, _arrowAttackPoint);
        _enemyBowAnim.Play(SUBBOW);
        yield return new WaitForSeconds(_EnemyBowAnimClip.length*_enemyAnimSpeedModifier);
        _canMoveAgain = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerWeapon"))
        {
            StartDeath();
        }
        else if(other.TryGetComponent<DashBehaviour>(out DashBehaviour dashBehaviour))
        {   
            if(KillComboHandler.KillComboCounter >= 45)
            {
                StartDeath();
            }
            else if(KillComboHandler.KillComboCounter >= 10)
            {
                dashBehaviour.TurnOffDashKill();
                StartDeath();
            }
        }
    }

    public void StopEnemy(bool comesBack)
    {
        _enemyAnim.enabled = false;
        _aIPath.enabled = false;
        locked = true;
        if(comesBack)
        {
            if(KillComboHandler.KillComboCounter > 50)
            {
                _wallStopper.enabled = true;
            }
            StartCoroutine(BringItBack());
        }
    }

    IEnumerator BringItBack()
    {
        yield return new WaitForSeconds(0.2f);
        _enemyAnim.enabled = true;
        _aIPath.enabled = true;
        _wallStopper.enabled = false;
        locked = false;
        myRb.velocity = new Vector3(0,0,0);
    }

    public void Thanosnaped()
    {
        InGameLevelManager.Instance.EnemyHasDied();
        Destroy(gameObject);
    }

    public void StartDeath()
    {
        if(!alreadyDying)
        {
            alreadyDying = true;
            StartCoroutine(ActualDeath());
        }
    }
    
    private IEnumerator ActualDeath()
    {
        SoundManager.Instance.PlaySound3D(SoundManager.Sound.EnemyDeath, transform.position);

        while (!explodePosChange)
        {
            yield return null;
        }

        PiecesHandler tempPiecesHandler = EnemyManager.Instance.GetAnDeadEnemy(transform);
        if(tempPiecesHandler != null)
        {
            tempPiecesHandler.StartExplode(_explodePoint.position);
        }

        _aIPath.canMove = false;
        _aIDestinationSetter.enabled = false;
        _enemyWeaponBehaviour.enabled = false;
        _enemyAnim.enabled = false;
        
        InGameLevelManager.Instance.EnemyHasDied();
        Destroy(gameObject);
    }

    public void UpdateExplodePoint(Vector3 newPoint, bool defaultPoint)
    {
        if(!defaultPoint)
        {
            _explodePoint.position = newPoint; 
        }
        explodePosChange = true;
    }
}
