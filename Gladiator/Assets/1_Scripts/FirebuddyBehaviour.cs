using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebuddyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _blueFireBall; 
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Fire Buddies")]
    [SerializeField] private GameObject _fireBuddyOne;
    [SerializeField] private GameObject _fireBuddyTwo;
    [SerializeField] private GameObject _fireBuddyThre;
    private bool _fireBuddyOneReady;
    private bool _fireBuddyTwoReady;
    private bool _fireBuddyThreeReady;

    private List<Transform> _alreadyTargeted;

    [Header("Control Variables")]
    public float _nextTotalTime;
    private float _nextAttacktimer;
    float closestDistance = Mathf.Infinity;
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

            }
            if(_fireBuddyThreeReady)
            {

            }

            _nextAttacktimer = _nextTotalTime;
        }
        _nextAttacktimer -= Time.deltaTime;
    }

    private void GoToShoot(Transform targetBuddy)
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(targetBuddy.position, 15, Vector3.forward, 1, _enemyLayer);
        float distance = 0;
        if(raycastHits.Length != 0)
        {
            foreach (var hit in raycastHits)
            {
                distance = Vector3.Distance(targetBuddy.position, hit.transform.position);
                if(distance < closestDistance)
                {
                    if(CheckIfAlreadyInUse(hit.transform))
                    {
                        closestDistance = distance;
                        Debug.Log(hit.transform.position);
                        closestObj = hit.transform.position;
                        _alreadyTargeted.Add(hit.transform);
                    }
                }
            }
            Vector3 direction = targetBuddy.transform.position - closestObj;

            targetBuddy.transform.LookAt(closestObj);
            Rigidbody rb = Instantiate(_blueFireBall, targetBuddy.position, transform.rotation).GetComponent<Rigidbody>();

            rb.velocity = targetBuddy.transform.forward * 40;
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
