using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    private enum WeaponType 
    {
        Melee, Hammer, Projectile
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
        if(_weaponType == WeaponType.Melee || _weaponType == WeaponType.Hammer)
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
                        if(KillComboHandler.KillComboCounter < 150)
                        {
                            playerHealth.TakeDamage(WeaponDamage);
                        }
                    }
                    else
                    {
                        playerHealth.TakeDamage(WeaponDamage);
                    }
                }
                else if(_weaponType == WeaponType.Hammer)
                {
                    playerHealth.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);
                    playerHealth.TakeDamage(WeaponDamage);
                    StartCoroutine(HitDelay());
                }
            }
        }
        if(_weaponType == WeaponType.Projectile)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Activate()
    {
        if(_weaponType != WeaponType.Hammer)
        {
            yield return new WaitForSeconds(0.45f);
            _weaponCollider.enabled = true;
            yield return new WaitForSeconds(0.65f);
            _weaponCollider.enabled = false;
        }
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.3f);
        alreadyHit = false;
    }
}
