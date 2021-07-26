using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;

    float h;
    float v;
    bool isHorizonMove;

    Rigidbody2D rigidbody;
    Animator anim;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        if (hDown || vUp) {
            isHorizonMove = true;
        } else if (vDown || hUp) {
            isHorizonMove = false;
        }

        // Animation
        if (anim.GetInteger("hAxisRaw") != h) {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        } else if (anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        } else {
            anim.SetBool("isChange", false);
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        rigidbody.velocity = moveVec * speed;
    }
}
