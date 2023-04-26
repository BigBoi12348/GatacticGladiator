using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumePlay : MonoBehaviour
{
    public GameObject music;
    public GameObject pause;
    [SerializeField] private FirstPersonController _firstPersonController;

    public void Plays()
    {
        GameManager.Instance.UnFreezeGame();
        if(_firstPersonController != null)
        {
            _firstPersonController.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        pause.SetActive(false);
        music.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Plays();
        }
    }
}
