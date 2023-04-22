using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseControls : MonoBehaviour
{

    public GameObject _controls;
    bool locked;
    private void OnEnable()
    {
        Debug.Log("Here");
        locked = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!locked)
        {
            locked = true;
            this.enabled = false;
        }
    }
}
