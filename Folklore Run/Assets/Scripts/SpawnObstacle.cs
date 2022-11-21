using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public string spawnPointTag = "ObstacleSpawner";
    public List<GameObject> obstacleList;


    private void Start()
    {
        if (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);

        Spawn();
    }

    private void Spawn()
    {
        int randomPrefab = Random.Range(0, obstacleList.Count);

        GameObject pts = Instantiate(obstacleList[randomPrefab], transform);


    }
}
