using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blow : MonoBehaviour
{
    public GameObject radius;
    // Start is called before the first frame update
    void Start()
    {
        radius.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            radius.SetActive(true);
        }
        else
        {
            radius.SetActive(false);
        }
        //if (Input.GetKey(KeyCode.C))
        //{
            //Physics.gravity = new Vector3(0,1, 0); 
        //}

        //if (Input.GetKey(KeyCode.V))
        //{
            //Physics.gravity = new Vector3(0, -9.81f, 0);
       // }

        //{
        //    Physics.gravity = new Vector3(0, -9.81f, 0);
        //}
    }
}
