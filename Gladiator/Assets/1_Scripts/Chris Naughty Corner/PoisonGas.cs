using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    bool buringPlayer;
    Coroutine burnCo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            buringPlayer = true;
            burnCo = StartCoroutine(BurnPlayer(playerHealth));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            buringPlayer = false;
            StopCoroutine(burnCo);
        }
    }

    IEnumerator BurnPlayer(PlayerHealth playerHealth)
    {
        while (buringPlayer)
        {
            yield return new WaitForSeconds(0.8f);
            playerHealth.TakeDamage(1);
        }
    }
}
