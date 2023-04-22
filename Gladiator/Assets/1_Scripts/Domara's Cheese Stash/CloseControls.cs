using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseControls : MonoBehaviour
{
    bool locked;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !locked)
        {
            locked = true;
            GetComponent<Image>().enabled = false;
        }
    }

    public void LockMe()
    {
        locked = false;
    }
}
