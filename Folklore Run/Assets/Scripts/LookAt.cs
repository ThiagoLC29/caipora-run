using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;


    void Start()
    {
        target = Camera.main.transform;
        //transform.rotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);

        //transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.x, -transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z);

        transform.rotation = target.rotation; //better than LookAt() for this exact purpose


    }
}
