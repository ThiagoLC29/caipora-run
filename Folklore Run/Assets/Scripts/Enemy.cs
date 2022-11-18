using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController controller;


    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (controller.isAttacking)
                Die();
            else
                Attack();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        Debug.Log("Enemy attacked you!");
    }

}
