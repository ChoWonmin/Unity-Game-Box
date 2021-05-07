using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finish : MonoBehaviour
{
    Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void open()
    {
        Debug.Log("open");
        animator.SetTrigger("Opening");
    }


}
