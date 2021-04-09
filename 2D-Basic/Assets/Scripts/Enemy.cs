using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;

    public int velocity;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        Think();
        Invoke("Think", Random.Range(3, 6));
    }
    private void FixedUpdate()
    {
        float offset = rigidbody.position.x + (velocity * 0.3f);
        Vector2 frontVec = new Vector2(offset, rigidbody.position.y);

        Debug.DrawRay(frontVec, Vector2.down*2, new Color(0, 0, 1));
        RaycastHit2D raycastHit = Physics2D.Raycast(frontVec, Vector2.down, 2, LayerMask.GetMask("Platform"));
        if (raycastHit.collider == null)
        {
            Turn();
        }

        rigidbody.velocity = new Vector2(velocity, rigidbody.velocity.y);
    }

    void Think()
    {
        //[warn] min <= range < max;
        velocity = Random.Range(-1, 2);

        animator.SetInteger("velocity", velocity);

        Invoke("Think", Random.Range(3, 6));
    }

    void Turn()
    {
        velocity = -velocity;
        spriteRenderer.flipX = velocity == 1;

        CancelInvoke();
        Invoke("Think", Random.Range(3, 6));
    }

    public void OnDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        circleCollider2D.enabled = false;
        rigidbody.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        Invoke("DeActive", 5);
    }

    private void DeActive()
    {
        gameObject.SetActive(false);
    }
}
