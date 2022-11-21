using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public enum PowerType { Boitata, Boto, Javali};
    public PowerType type;
    public PlayerController controller;

    void Start()
    {
        controller = FindObjectOfType<PlayerController>();

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (type)
            {
                case PowerType.Boitata:
                    StartCoroutine(controller.Boitata());
                    break;
                case PowerType.Boto:
                    StartCoroutine(controller.Boto());
                    break;
                case PowerType.Javali:
                    StartCoroutine(controller.Javali());
                    break;
                default:
                    break;
            }

        }
    }

}
