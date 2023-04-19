using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourRandomizer : MonoBehaviour
{
    private Renderer objectRenderer; 
    public float interval = 10.0f;  

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>(); 
        if (objectRenderer != null)
        {
            StartCoroutine(RandomizeColorRoutine());
        }
    }

    private IEnumerator RandomizeColorRoutine()
    {
        while (true)
        {
            RandomizeColor(); 
            yield return new WaitForSeconds(10);
        }
    }

    private void RandomizeColor()
    {
        float red = Random.Range(0.0f, 1.0f);
        float green = Random.Range(0.0f, 1.0f); 
        Color newColor = new Color(red, green, 0.0f, 1.0f); 
        objectRenderer.material.color = newColor;  
    }
}

