using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlsPlay : MonoBehaviour
{
    public CloseControls closeControls;
    public GameObject controls;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Plays()
    {
        closeControls.enabled = true;
    }
}
