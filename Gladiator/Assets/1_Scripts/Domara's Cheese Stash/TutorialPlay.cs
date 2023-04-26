using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialPlay : MonoBehaviour
{
    public GameObject hoverSound;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Plays()
    {
        GameManager.Instance.LoadThisScene(GameManager.TUTSCENE);
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
