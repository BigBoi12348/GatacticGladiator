using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBattle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.LoadThisScene(GameManager.GAMESCENE);
    }
}
