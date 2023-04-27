using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableThis : MonoBehaviour
{
    public GameObject Enable;
    public GameObject Disable;
    public GameObject displayDisable;
    private void OnTriggerEnter(Collider other)
    {
        Enable.SetActive(true);
        Disable.SetActive(false);
        displayDisable.SetActive(false);
    }
}
