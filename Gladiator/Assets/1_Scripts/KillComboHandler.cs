using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillComboHandler : MonoBehaviour
{
    public static int KillComboCounter{get; private set;}
    
    public void AddToCombo(int value)
    {
        KillComboCounter += value;
    }
}
