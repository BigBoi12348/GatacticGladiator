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

    private void Start() 
    {
        _alreadyTargeted = new List<Transform>();
    }

    void Update()
    {
        if(_nextAttacktimer < 0)
        {
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
        
        foreach (var item in raycastHits)
        {
            Debug.Log(item.transform.position);
        }

        Rigidbody rb = Instantiate(_blueFireBall, targetBuddy.position, transform.rotation).GetComponent<Rigidbody>();
    }
}
