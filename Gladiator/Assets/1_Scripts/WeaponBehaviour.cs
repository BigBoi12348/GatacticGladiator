using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public bool IsInCollision{get; private set;}

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Collided");
        IsInCollision = true;
    }

    private void OnCollisionExit(Collision other) 
    {
        IsInCollision = false;
    }
}
