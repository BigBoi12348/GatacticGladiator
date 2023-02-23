using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSceneManager : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.LoadThisScene(1);
    }
}
