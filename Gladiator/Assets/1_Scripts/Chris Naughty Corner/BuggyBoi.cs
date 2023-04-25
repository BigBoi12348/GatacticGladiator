using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuggyBoi : MonoBehaviour
{
    
    public GameObject ad;

    void Spawn()
    {
        ad.SetActive(true);
    }
   void Despawn()
    {
        ad.SetActive(false);
    }
}
