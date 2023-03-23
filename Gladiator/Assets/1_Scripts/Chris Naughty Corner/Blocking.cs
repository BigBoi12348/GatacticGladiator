using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocking : MonoBehaviour
{
    public GameObject unBlocked;
    public GameObject Blocked;
    public Collider playerCollider;
    public Slider slider;
    public float decreaseSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            unBlocked.SetActive(false);
            Blocked.SetActive(true);
            playerCollider.enabled = false;
        }
        else
        {
            unBlocked.SetActive(true);
            Blocked.SetActive(false);
            playerCollider.enabled = true;
        }

        if (Input.GetMouseButton(1))
        {
            slider.value -= decreaseSpeed * Time.deltaTime;
        }
        if(slider.value <= 0)
        {
            Blocked.SetActive(false);
            unBlocked.SetActive(false);
            playerCollider.enabled = true;
        }
    


}
}
