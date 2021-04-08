using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    public Manager manager;
    Rigidbody rigidBody;
    AudioSource audio;

    int score = 0;
    bool isJump = false;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rigidBody.AddForce(Vector3.up * 25, ForceMode.Impulse);
            isJump = true;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigidBody.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Can")
        {
            other.gameObject.SetActive(false);
            score++;
            manager.updateScoreText(score);
            audio.Play();
        }

        if (other.tag == "Finish")
        {
            manager.checkClearSatage(score);
        }
    }
}
