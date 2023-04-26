using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Quit : MonoBehaviour
{
    public GameObject hoverSound;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Quite()
    {
        Debug.Log("Quit!");
        Application.Quit();
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
