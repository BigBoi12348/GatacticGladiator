using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] private float _enemyMovementSpeed;
    [SerializeField] private float _enemyMovementSpeedModifier;
    

    [Header("Bleed Out values")]
    private bool _bodyPartMissing;
    [SerializeField] private float _totalBleedOut;
    [SerializeField] private float _bleedOutStrength;
    [SerializeField] private float _bleedOutModifier;
    
    [Header("Body Parts")]
    [SerializeField] private EnemyBodyPart[] enemyBodyParts;
    
    private void Awake() 
    {
        foreach (var enemyBodyPart in enemyBodyParts)
        {
            enemyBodyPart.Init(this);
        }
    }

    private void Update()
    {
        if(_bodyPartMissing)
        {
            if(_totalBleedOut > 0)
            {
                _totalBleedOut -= _bleedOutStrength + _bleedOutModifier;
            }
            else
            {
                Death();
            }
        }
    }


    public void BodyPartLost(bool doIDie, float bleedingStrength)
    {
        if(doIDie)
        {
            Death();
        }
        else
        {
            _bodyPartMissing = true;
            _bleedOutModifier += bleedingStrength;
        }
    }

    private void Death()
    {
        
    }
}
