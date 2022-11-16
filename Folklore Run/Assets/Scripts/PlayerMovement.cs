using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float moveSpeed = 3f;
    public float maxSpeed = 100f;
    public float speedIncrease = 0.01f;
    public Vector3 direction = Vector3.forward;


    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (moveSpeed <= maxSpeed)
        {
            moveSpeed += speedIncrease;
        }

    }


}
