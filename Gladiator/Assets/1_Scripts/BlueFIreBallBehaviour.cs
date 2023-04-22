using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFIreBallBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
    }
}
