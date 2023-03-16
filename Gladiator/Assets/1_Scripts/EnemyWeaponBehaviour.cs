using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider _weaponCollider;
    bool alreadyHit;

    private void Start() 
    {
        _weaponCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.name);
        if(!alreadyHit)
        {
            if(other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                alreadyHit = true;
                playerHealth.TakeDamage(15);
                StartCoroutine(HitDelay());
            }
        }
    }

    public IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        _weaponCollider.enabled = true;
        yield return new WaitForSeconds(0.6f);
        _weaponCollider.enabled = false;
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(0.3f);
        alreadyHit = false;
    }
}
