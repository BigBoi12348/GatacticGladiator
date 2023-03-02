using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void Plays()
    {
        GameManager.Instance.LoadThisScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
