using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPause : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private FirstPersonController _firstPersonController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.FreezeGame();
            if(_firstPersonController != null)
            {
                _firstPersonController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            
            panel.SetActive(true);
        }
    }
}
