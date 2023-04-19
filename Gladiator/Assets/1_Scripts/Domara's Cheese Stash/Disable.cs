using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    public GameObject UI;
    private void OnTriggerEnter(Collider other)
    {
        UI.SetActive(false);
    }
}
