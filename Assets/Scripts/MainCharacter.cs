﻿using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Rigidbody rb;
    float input;
    bool touchedWall;
    ParticleSystem trailParticles;

    [SerializeField] float speed, force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailParticles = transform.GetChild(0).GetComponent<ParticleSystem>();       
    }

    void FixedUpdate()
    {
        input = Input.GetAxis("Horizontal");

        if (input != 0 && !touchedWall)
        {
            rb.angularVelocity = Vector3.back * (input * speed);
            rb.AddForce(Vector3.right * force * input, ForceMode.Impulse);
        }

        if (rb.velocity.y > 2)
            trailParticles.Play();
        else
            trailParticles.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreTrigger"))
        {
            other.gameObject.SetActive(false);
            LevelManager.instance.IncrementScore();
        }

        if (other.CompareTag("GameOver"))
            LevelManager.instance.Fail();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Wall"))
            touchedWall = true;
        else
            touchedWall = false;
    }
}
