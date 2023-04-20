using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : MonoBehaviour
{
    [SerializeField] private Collider _dashCol;

    private void Start() 
    {
        if(PlayerUpgradesData.StarOne)
        {
            gameObject.SetActive(true);
            _dashCol.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void StartDash()
    {
        if(KillComboHandler.KillComboCounter >= 10)
        {
            _dashCol.enabled = true;
        }
        else
        {
            _dashCol.enabled = false;
        }
    }

    public void TurnOffDashKill()
    {
        _dashCol.enabled = false;
    }
}
