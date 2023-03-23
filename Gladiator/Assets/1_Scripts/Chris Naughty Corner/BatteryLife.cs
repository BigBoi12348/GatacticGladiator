using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryLife : MonoBehaviour
{
    public Slider slider;
    public float decreaseSpeed = 1f;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            slider.value -= decreaseSpeed * Time.deltaTime;
        }
    }
}
