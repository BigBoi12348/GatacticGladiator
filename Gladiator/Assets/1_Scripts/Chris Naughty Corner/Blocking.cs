using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    public GameObject unBlocked;
    public GameObject Blocked;
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
        }
        else
        {
            unBlocked.SetActive(true);
            Blocked.SetActive(false);
        }
    }
}
