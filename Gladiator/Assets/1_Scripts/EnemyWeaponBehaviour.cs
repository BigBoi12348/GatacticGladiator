using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    private enum WeaponType 
    {
        Melee, Projectile
    }
    [SerializeField] private WeaponType _weaponType;
    //[SerializeField] private bool isWeaponAlreadyActive;
    [Header("References")]
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private int WeaponDamage;
    bool alreadyHit;

    private void Start() 
    {
        if(_weaponType == WeaponType.Melee)
        {
            _weaponCollider.enabled = false;
        }
        alreadyHit = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!alreadyHit)
        {
            if(other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                alreadyHit = true;
                PostProcessingEffectManager.Instance.HurtEffect(0.1f);
                playerHealth.TakeDamage(WeaponDamage);
                if(_weaponType == WeaponType.Melee)
                {
                    StartCoroutine(HitDelay());
                }
                else if(_weaponType == WeaponType.Projectile)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.45f);
        _weaponCollider.enabled = true;
        yield return new WaitForSeconds(0.65f);
        _weaponCollider.enabled = false;
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.3f);
        alreadyHit = false;
    }
}
