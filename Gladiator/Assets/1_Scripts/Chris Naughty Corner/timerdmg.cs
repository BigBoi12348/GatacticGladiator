using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerdmg : MonoBehaviour
{
    
    public GameObject Gas;

    void RemoveGas()
    {
        Gas.SetActive(false);
    }
    void SpawnGas()
    {
        Gas.SetActive(true);
    }
}
