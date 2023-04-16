using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PiecesHandler : MonoBehaviour
{
    [Header("Explosion Control Variables")]
    public float explosionForce = 10.0f;
    public float explosionRadius = 5.0f;
    private Vector3 explosionPosition;

    [Header("References")]
    [SerializeField] private Rigidbody[] rbs;
    private EnemyManager _enemyManager;

    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }

    public void StartExplode(Transform pointOfExplosion)
    {
        explosionPosition = pointOfExplosion.position;
        Explode();
    }

    private void Explode()
    {
        foreach (var rb in rbs)
        {
            rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }
}
