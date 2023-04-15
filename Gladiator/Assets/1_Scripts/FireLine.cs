using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireArms;
    private ParticleSystem.EmissionModule _fireEmission;
    const int RAGE = 200;

    void Start()
    {
        _fireEmission = _fireArms.emission;
    }
    
    public void TurnOn()
    {
        _fireEmission.rateOverTime = RAGE;
    }

    public void TurnOff()
    {
        _fireEmission.rateOverTime = 0;
    }
}
