using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finish : MonoBehaviour
{
    Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Opening");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
