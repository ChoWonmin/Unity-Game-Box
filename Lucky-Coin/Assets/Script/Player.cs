using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    AudioSource audio;

    // sound
    public AudioClip audioDamaged;
    public AudioClip audioCoin;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // stop move
        if (Input.GetButtonUp("Vertical"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        // move
        float v = Input.GetAxisRaw("Vertical");
        rigidbody.AddForce(Vector2.up * v * 3 * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Debug.Log("En");
            OnDamaged();
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

        Invoke("OffDamaged", 2);
    }

    private void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Vector4(1, 1, 1, 1);
    }
}
