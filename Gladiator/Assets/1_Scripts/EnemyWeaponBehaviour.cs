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
    bool checkCombo;

    private void Start() 
    {
        if(_weaponType == WeaponType.Melee)
        {
            _weaponCollider.enabled = false;
        }
        alreadyHit = false;

        if(PlayerUpgradesData.StarFive)
        {
            checkCombo = true;
        }
        else
        {
            checkCombo = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(!alreadyHit)
        {
            if(other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                alreadyHit = true;
                if(_weaponType == WeaponType.Melee)
                {
                    playerHealth.TakeDamage(WeaponDamage);
                    StartCoroutine(HitDelay());
                }
                else if(_weaponType == WeaponType.Projectile)
                {
                    if(checkCombo)
                    {
                        if(KillComboHandler.KillComboCounter >= 80)
                        {
                            playerHealth.TakeDamage(WeaponDamage);
                        }
                    }
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
