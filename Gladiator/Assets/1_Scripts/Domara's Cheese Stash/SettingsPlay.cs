using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPlay : MonoBehaviour
{
    public GameObject hoverSound;
    public CloseSettings closeSettings;
    public Image settings;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Plays()
    {
        settings.enabled = true;
        closeSettings.LockMe();
    }
    public void HoverOver()
    {
        hoverSound.SetActive(true);
    }
    public void HoverOff()
    {
        hoverSound.SetActive(false);
    }
}
