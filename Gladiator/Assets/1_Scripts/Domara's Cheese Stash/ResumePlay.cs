using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumePlay : MonoBehaviour
{
    public GameObject pause;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Plays()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
    }
}
