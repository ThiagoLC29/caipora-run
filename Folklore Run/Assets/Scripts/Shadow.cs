using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public PlayerController controller;
    Transform stay;
    public float jumpingSize = 0.5f;

    void Awake()
    {
        stay = transform;
        controller = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.rotation = stay.rotation;

        if (controller.isJumping)
            transform.localScale = new Vector3(jumpingSize, jumpingSize, jumpingSize);
        else
            transform.localScale = Vector3.one;


        if (controller.isSliding)
        {
            transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
            transform.localPosition = new Vector3(transform.position.x, 0.25f, -0.45f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            transform.position = new Vector3(controller.transform.position.x, -0.95f, controller.transform.position.z);
        }
            
    }
}
