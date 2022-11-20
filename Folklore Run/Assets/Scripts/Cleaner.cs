using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    // the name is cleaner because it was used for something else earlier on this project

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground" && other.tag != "Spawner" && other.tag != "Collectable")
            Destroy(other.gameObject);
    }

}
