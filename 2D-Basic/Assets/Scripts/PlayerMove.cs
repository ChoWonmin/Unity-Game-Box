using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    AudioSource audioSource;

    // sound
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioCoin;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    public float maxSpeed;
    public float jumpPower;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.normalized.x * 0.5f, rigidbody.velocity.y);
        }

        // Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            bool direction = Input.GetAxisRaw("Horizontal") == -1;
            spriteRenderer.flipX = direction;
        }

        // Stop Animation
        if (Mathf.Abs(rigidbody.velocity.x) < 0.3)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            PlaySound("JUMP");
        }
    }

    private void FixedUpdate()
    {
        // Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // Max Speed
        if (rigidbody.velocity.x > maxSpeed)
        {
            rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
        } else if (rigidbody.velocity.x < -maxSpeed)
        {
            rigidbody.velocity = new Vector2(-maxSpeed, rigidbody.velocity.y);
        }

        // Landing Platform
        if (rigidbody.velocity.y < 0)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (raycastHit.collider != null && raycastHit.distance < 0.7f)
            {
                animator.SetBool("isJumping", false);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            // Attack :: 낙하하면서 몬스터 위에 있는 경우
            if (rigidbody.velocity.y < 0 && rigidbody.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            } else
            {
                OnDamaged(collision.collider.transform.position);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (collision.gameObject.name.Contains("Bronze"))
            {
                gameManager.stagePoint += 50;
            } else if (collision.gameObject.name.Contains("Silver"))
            {
                gameManager.stagePoint += 100;
            } else if (collision.gameObject.name.Contains("Gold"))
            {
                gameManager.stagePoint += 300;
            }

            collision.gameObject.SetActive(false);
            PlaySound("COIN");
        }
        else if (collision.gameObject.tag == "Finish")
        {
            gameManager.NextStage();
            PlaySound("FINISH");
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "COIN":
                audioSource.clip = audioCoin;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }

    private void OnAttack(Transform enemyTransform)
    {
        gameManager.stagePoint += 150;

        rigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.OnDamaged();
        PlaySound("ATTACK");
    }

    private void OnDamaged(Vector2 enemyPosition)
    {
        gameObject.layer = 11;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dircetion = rigidbody.position.x - enemyPosition.x > 0 ? 1:-1;
        rigidbody.AddForce(new Vector2(dircetion, 1) * 8, ForceMode2D.Impulse);

        animator.SetTrigger("doDamaged");
        gameManager.HealthDown();
        PlaySound("DAMAGED");

        Invoke("offDamaged", 2);
    }

    private void offDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void onDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        rigidbody.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        capsuleCollider.enabled = false;
        PlaySound("DIE");
    }

    public void Reposition()
    {
        rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(0, 3, 0);
    }

}
