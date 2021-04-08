using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;
    
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = playerTransform.position - transform.position;
    }
    private void LateUpdate()
    {
        transform.position = playerTransform.position - offset;
    }

}
