using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSwitcher : MonoBehaviour
{
    public GameObject waveText;
    private void Start()
    {
        Invoke("WaveText", 3);
    }
    void WaveText()
    {
        waveText.SetActive(false);
    }
}
