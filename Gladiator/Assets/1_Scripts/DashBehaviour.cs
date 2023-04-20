using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : MonoBehaviour
{
    [SerializeField] private Collider _dashCol;
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
