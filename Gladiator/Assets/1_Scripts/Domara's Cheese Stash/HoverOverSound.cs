using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOverSound : MonoBehaviour
{
    public GameObject hoverSound;
    public void HoverOver()
    {
        hoverSound.SetActive(true);
    }
    public void HoverOff()
    {
        hoverSound.SetActive(false);
    }
}
