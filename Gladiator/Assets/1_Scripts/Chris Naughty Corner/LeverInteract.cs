using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    public GameObject lever;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Mouse0))
    //    //{
    //    //    animator.Play("AsD");
    //    //}

    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            animator.Play("AsD");
        }
       
    }
    void Stay()
    {
        lever.SetActive(true);
    }
    void Gone()
    {
        lever.SetActive(false);
    }
}

