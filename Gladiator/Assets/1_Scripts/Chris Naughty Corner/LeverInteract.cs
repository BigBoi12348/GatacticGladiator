using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    animator.Play("AsD");
        //}

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && player /*&& other.CompareTag("Player")*/)
        {
            animator.Play("AsD");
        }
        
    }
}

