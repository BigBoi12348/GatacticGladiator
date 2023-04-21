using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrowBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyWeaponBehaviour _enemyWeaponBehaviour;
    private Transform _arrowAttackPoint;
    private float _arrowSpeed;

    public void Init(float arrowSpeed, Transform arrowAttackPoint)
    {
        _arrowSpeed = arrowSpeed;
        _arrowAttackPoint = arrowAttackPoint;
    }
    
    private void ForwardPos()
    {
        transform.eulerAngles = new Vector3(0,87,0);
    }

    private void ShootArrow()
    {
        Rigidbody rb = Instantiate(_enemyWeaponBehaviour, _arrowAttackPoint.position, transform.parent.rotation * Quaternion.Euler(0,180,0)).GetComponent<Rigidbody>();
        rb.AddForce(transform.parent.forward * _arrowSpeed, ForceMode.Impulse);
    }

    private void NormalPos()
    {
        transform.eulerAngles = new Vector3(0,25,0);
    }
}
