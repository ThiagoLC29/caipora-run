using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;


    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.x, -transform.rotation.eulerAngles.y, 0f);
    }
}
