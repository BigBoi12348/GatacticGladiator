using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider _weaponCollider;
    [SerializeField] private int WeaponDamage;
    bool alreadyHit;

    private void Start() 
    {
        _weaponCollider.enabled = false;
        alreadyHit = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.gameObject);
        if(!alreadyHit)
        {
            Debug.Log(other.gameObject.name);
            if(other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                alreadyHit = true;
                PostProcessingEffectManager.Instance.HurtEffect(0.1f);
                playerHealth.TakeDamage(WeaponDamage);
                StartCoroutine(HitDelay());
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
