using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableThis : MonoBehaviour
{
    public GameObject thing;
    public GameObject bye;
    private void OnTriggerEnter(Collider other)
    {
        thing.SetActive(true);
        bye.SetActive(false);
    }
}
