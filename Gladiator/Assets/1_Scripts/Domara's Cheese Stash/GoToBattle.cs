using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBattle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.LoadThisScene(GameManager.GAMESCENE);
        }
    }
}
