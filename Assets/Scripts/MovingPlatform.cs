﻿using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float Speed;
    [SerializeField] private Transform rPivot, lPivot;
    //float moveChance;

    void Start()
    {

        //moveChance = Random.Range(0, 10); //This is needed to set the chance of a moving platform to spawn       
        var distance = Random.Range(1f, 7f);

        rPivot.localScale += Vector3.right * distance;
        lPivot.localScale += Vector3.left * distance;
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * Speed;

        /* if (moveChance >= 7)
        {
            rPivot.localScale += Vector3.right * 0.05f * Mathf.Sin(Time.time);
            lPivot.localScale += Vector3.left* 0.05f * Mathf.Sin(Time.time);
        } */
    }
}