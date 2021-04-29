using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocity;

    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(-velocity * Time.deltaTime, rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            transform.position = new Vector3(49, Random.Range(-5,6));
        }
    }

}
