using System.Collections;
using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float moveSpeed = 3f;
    public float maxSpeed = 100f;
    public float speedIncrease = 0.01f;
    public Vector3 direction = Vector3.forward;
    public PlayerController controller;


    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (controller.isPaused == false)
            Move();
    }

    public void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (moveSpeed <= maxSpeed)
        {
            moveSpeed += speedIncrease;
        }

        controller.score = (int)Math.Ceiling(controller.score + 1 * Time.deltaTime); //adds score rounding up
    }


}
