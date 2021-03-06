using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject rainbowRight;
    public GameObject rainbowLeft;

    public float speed;
    public float stopSpeed;
    public float maxSpeed;

    int health = 3;
    bool isStop = false;

    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    AudioSource audio;
    CapsuleCollider2D capsuleCollider;
    Animator animator;


    // sound
    public AudioClip audioDamaged;
    public AudioClip audioCoin;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    public Sprite damagedSprite;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // stop move
        if (Input.GetButtonUp("Vertical"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * stopSpeed);
            animator.SetBool("isWalking", false);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x * stopSpeed, rigidbody.velocity.y);
            animator.SetBool("isWalking", false);
        }

        // walking animation
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {

            // rainbow.transform.position.Set(direction?-1:1, 0, 0);
            if (rigidbody.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
                rainbowLeft.SetActive(true);
                rainbowRight.SetActive(false);
            } else
            {
                spriteRenderer.flipX = false;
                rainbowLeft.SetActive(false);
                rainbowRight.SetActive(true);
            }


            animator.SetBool("isWalking", true);
        }

    }

    private void FixedUpdate()
    {
        // move
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (!isStop)
        {
            rigidbody.AddForce(Vector2.up * v * speed * Time.deltaTime, ForceMode2D.Impulse);
            rigidbody.AddForce(Vector2.right * h * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

        // Max Speed
        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
        {
            rigidbody.velocity = new Vector2(
                maxSpeed * Mathf.Sign(rigidbody.velocity.x), 
                rigidbody.velocity.y
            );
        }

        if (Mathf.Abs(rigidbody.velocity.y) > maxSpeed)
        {
            rigidbody.velocity = new Vector2(
                rigidbody.velocity.x,
                maxSpeed* Mathf.Sign(rigidbody.velocity.y)
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            OnDamaged();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            GameObject coin = collision.gameObject;
            PlaySound("COIN");
            gameManager.addScore(coin.GetComponent<Coin>());

            coin.SetActive(false);
        }

        if (collision.gameObject.tag == "Finish")
        {
            collision.gameObject.GetComponent <Finish>().open();
            gameObject.layer = 9;

            isStop = true;
            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.gravityScale = 0;


            PlaySound("FINISH");
            gameManager.finish();
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "DAMAGED":
                audio.clip = audioDamaged;
                break;
            case "COIN":
                audio.clip = audioCoin;
                break;
            case "DIE":
                audio.clip = audioDie;
                break;
            case "FINISH":
                audio.clip = audioFinish;
                break;
        }

        audio.Play();
    }

    private void OnDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Vector4(1, 1, 1, 0.4f);
        PlaySound("DAMAGED");
        health--;
        animator.SetBool("isDamaged", true);
        gameManager.deactiveHealth(health);

        ((SpriteRenderer)rainbowLeft.GetComponent<SpriteRenderer>()).color = new Vector4(1, 1, 1, 0.0f);
        ((SpriteRenderer)rainbowRight.GetComponent<SpriteRenderer>()).color = new Vector4(1, 1, 1, 0.0f);

        if (health < 1) // Die
        {
            gameManager.openRestart();
            OnDie();
        } else
        {
            Invoke("OffDamaged", 2);
        }
    }

    private void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Vector4(1, 1, 1, 1);
        animator.SetBool("isDamaged", false);

        ((SpriteRenderer)rainbowLeft.GetComponent<SpriteRenderer>()).color = new Vector4(1, 1, 1, 1);
        ((SpriteRenderer)rainbowRight.GetComponent<SpriteRenderer>()).color = new Vector4(1, 1, 1, 1);
    }

    private void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        rigidbody.gravityScale = 1;
        capsuleCollider.enabled = false;
        PlaySound("DIE");
    }
}
