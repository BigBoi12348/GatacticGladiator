using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireArms;
    [SerializeField] private BoxCollider boxCol;
    private ParticleSystem.EmissionModule _fireEmission;
    const int RAGE = 200;
    bool buringPlayer;
    Coroutine burnCo;

    void Start()
    {
        boxCol.enabled = false;
        _fireEmission = _fireArms.emission;
    }
    
    public void TurnOn()
    {
        StartCoroutine(ColliderState(0.5f, true));
        _fireEmission.rateOverTime = RAGE;
    }

    public void TurnOff()
    {
        _fireEmission.rateOverTime = 0;
        StartCoroutine(ColliderState(0f, false));
        buringPlayer = false;
        if(burnCo != null)
        {
            StopCoroutine(burnCo);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            buringPlayer = true;
            burnCo = StartCoroutine(BurnPlayer(playerHealth));
        }
    }

    IEnumerator BurnPlayer(PlayerHealth playerHealth)
    {
        while (buringPlayer)
        {
            yield return new WaitForSeconds(0.1f);
            playerHealth.TakeFireDamage(5);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            buringPlayer = false;
            StopCoroutine(burnCo);
        }
    }

    IEnumerator ColliderState(float delay, bool state)
    {
        yield return new WaitForSeconds(delay);
        boxCol.enabled = state;
    }
}
