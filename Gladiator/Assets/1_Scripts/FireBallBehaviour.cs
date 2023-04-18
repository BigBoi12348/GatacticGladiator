using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    [SerializeField] private Collider _col;
    private void Start()
    {
       Invoke("TurnOnCollider", 0.1f); 
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.name);
        if(other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeFireDamage(10);
            CameraEffectsSystem.Instance.ShakeCamera(2f, 0.2f);
        }
        Destroy(gameObject);
    }

    private void TurnOnCollider()
    {
        _col.enabled = true;
    }
}
