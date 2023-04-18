using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireLane : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireArms;
    [SerializeField] private BoxCollider boxCol;
    private ParticleSystem.EmissionModule _fireEmission;
    const int RAGE = 200;

    void Start()
    {
        _fireEmission = _fireArms.emission;
        boxCol.enabled = false;
        _fireEmission.rateOverTime = 0;
    }

    public void ShootThem()
    {
        _fireEmission.rateOverTime = RAGE;
        boxCol.enabled = true;
        StartCoroutine(BurnOut());
    }

    IEnumerator BurnOut()
    {
        yield return new WaitForSeconds(0.5f);
        boxCol.enabled = false;
        _fireEmission.rateOverTime = 0;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
