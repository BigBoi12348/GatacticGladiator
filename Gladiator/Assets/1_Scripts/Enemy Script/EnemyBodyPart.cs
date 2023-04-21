using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour _enemyBehaviour;
    [SerializeField] private MeshCollider _meshCol;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "PlayerWeapon")
        {
            Vector3 contact = _meshCol.ClosestPoint(other.ClosestPoint(other.transform.position));
            _enemyBehaviour.UpdateExplodePoint(contact, false);
        }
    }
}
