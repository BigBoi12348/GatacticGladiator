using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebuddyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _blueFireBall; 
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _bothEnemyLayers;
    private LayerMask _usedMask;

    [Header("Fire Buddies")]
    [SerializeField] private GameObject _fireBuddyOne;
    [SerializeField] private GameObject _fireBuddyTwo;
    [SerializeField] private GameObject _fireBuddyThree;
    private bool _fireBuddyOneReady;
    private bool _fireBuddyTwoReady;
    private bool _fireBuddyThreeReady;

    private List<Transform> _alreadyTargeted;

    [Header("Control Variables")]
    public float _nextTotalTime;
    private float _usedNextTotalTime;
    private float _nextAttacktimer;
    //float closestDistance = Mathf.Infinity;
    Vector3 closestObj;

    private void Awake() 
    {
        _alreadyTargeted = new List<Transform>();
    }

    private void Start() 
    {
        if(PlayerUpgradesData.ShieldFour)
        {
            _fireBuddyOne.SetActive(true);
            _fireBuddyOneReady = true;
        }
        if(PlayerUpgradesData.ShieldThree)
        {
            _fireBuddyTwoReady = true;
        }
        else
        {
            _usedNextTotalTime = _nextTotalTime;
        }
        if(PlayerUpgradesData.ShieldFive)
        {
            _fireBuddyThreeReady = true;
        }

        _usedMask = _bothEnemyLayers;
    }

    void Update()
    {
        if(_nextAttacktimer < 0)
        {
            _alreadyTargeted = new List<Transform>();
            if(_fireBuddyOneReady)
            {
                GoToShoot(_fireBuddyOne.transform);
            }
            if(_fireBuddyTwoReady)
            {
                if(KillComboHandler.KillComboCounter >= 60)
                {
                    _fireBuddyTwo.SetActive(true);
                    GoToShoot(_fireBuddyTwo.transform);
                }
                else
                {
                    _fireBuddyTwo.SetActive(false);
                }
            }
            
            if(_fireBuddyThreeReady)
            {
                if(KillComboHandler.KillComboCounter >= 100)
                {
                    _fireBuddyThree.SetActive(true);
                    GoToShoot(_fireBuddyThree.transform);
                }
                else
                {
                    _fireBuddyThree.SetActive(false);
                }
            }

            _nextAttacktimer = _nextTotalTime;
        }
        _nextAttacktimer -= Time.deltaTime;

        if(_fireBuddyTwoReady)
        {
            if(KillComboHandler.KillComboCounter >= 110)
            {
                _usedNextTotalTime = 2;
            }
            else
            {
                _usedNextTotalTime = _nextTotalTime;
            }
        }

        if(PlayerUpgradesData.StarFive)
        {
            if(KillComboHandler.KillComboCounter >= 60)
            {
                _usedMask = _enemyLayer;
            }
            else
            {
                _usedMask = _bothEnemyLayers;
            }
        }
    }

    private void GoToShoot(Transform targetBuddy)
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(targetBuddy.position, 40, Vector3.forward, 1, _enemyLayer);
        float distance = 0;
        if(raycastHits.Length != 0)
        {
            closestObj = Vector3.zero;
            float closestDistance = Mathf.Infinity;
            foreach (var hit in raycastHits)
            {
                distance = Vector3.Distance(targetBuddy.position, hit.transform.position);
                if(distance < closestDistance)
                {
                    if(CheckIfAlreadyInUse(hit.transform))
                    {
                        closestDistance = distance;
                        closestObj = hit.transform.position;
                        _alreadyTargeted.Add(hit.transform);
                    }
                }
            }
            Vector3 direction = targetBuddy.transform.position - closestObj;

            targetBuddy.transform.LookAt(closestObj);
            Rigidbody rb = Instantiate(_blueFireBall, targetBuddy.position, transform.rotation).GetComponent<Rigidbody>();

            rb.velocity = targetBuddy.transform.forward * 50;
        }
        else
        {
            Debug.Log("Fire Found nothing");
        }
    }

    private bool CheckIfAlreadyInUse(Transform newPos)
    {
        foreach (var already in _alreadyTargeted)
        {
            if(already == newPos)
            {
                return false;
            }
        }
        return true;
    }
}
