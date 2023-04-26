using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPause : MonoBehaviour
{
    public GameObject panel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            //Cursor.visible = false;
            panel.SetActive(true);
        }
    }
}
