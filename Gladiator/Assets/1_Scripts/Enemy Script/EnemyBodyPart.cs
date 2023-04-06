using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    private EnemyBehaviour _enemyBehaviour;

    [Header("Function")]
    [SerializeField] private bool _doIKillInstantly;
    [SerializeField] private float _myBleedOutStrength;

    public void Init(EnemyBehaviour enemyBehaviour)
    {
        _enemyBehaviour = enemyBehaviour;
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.name);

        if(other.gameObject.CompareTag("Player")) 
        {
            //GetComponent<MeshCollider>().enabled = false;
            _enemyBehaviour.BodyPartLost(_doIKillInstantly, _myBleedOutStrength);
            Destroy(this);
        }
    }

    public void DestroyMe() 
    {
        _enemyBehaviour.BodyPartLost(_doIKillInstantly, _myBleedOutStrength);
        Destroy(this);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     //if(other.name.Equals)
    //     {

    //     }
    // }
}
