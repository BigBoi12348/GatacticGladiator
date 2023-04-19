using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    private void Start()
    {
        Invoke("StartMenu", 65);
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
