using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANimation : MonoBehaviour
{
    private Animator animator;
    private bool isPlaying = false;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPlaying)
        {

            animator.SetTrigger("Slice");
            isPlaying = true;
            Debug.Log("hit");
        }
        //if (Input.GetMouseButtonDown(0) && !isPlaying)
        //{

        //    animator.SetTrigger("Slice");
        //    isPlaying = true;
        //    Debug.Log("hit");
        //}
    }

    public void AnimationFinished()
    {
        animator.SetTrigger("Attack");
        isPlaying = false;
    }
}
