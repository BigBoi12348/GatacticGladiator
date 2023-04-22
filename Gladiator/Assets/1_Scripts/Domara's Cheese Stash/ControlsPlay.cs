using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsPlay : MonoBehaviour
{
    public CloseControls closeControls;
    public Image controls;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Plays()
    {
        controls.enabled = true;
        closeControls.LockMe();
    }
}
