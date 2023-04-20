using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuFromCredits : MonoBehaviour
{
    private void Start()
    {
        Invoke("StartMenu", 62);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.LoadThisScene(GameManager.MAINMENUSCENE);
        }
    }
    void StartMenu()
    {
        GameManager.Instance.LoadThisScene(GameManager.MAINMENUSCENE);
    }
}
