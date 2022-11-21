using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGround : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject spawnPoint;
    public PlayerMovement playerMove;
    public PlayerController playerCon;

    public string spawnPointTag = "GroundSpawn";
    public List<GameObject> groundList;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMovement>();
        playerCon = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Spawn();
        }
    }

    private void Spawn()
    {

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int randomPrefab = Random.Range(0, groundList.Count);
            Debug.Log("randomPrefab is: " + randomPrefab);
            GameObject pts = Instantiate(groundList[randomPrefab]);
            pts.transform.position = spawnPoint.transform.position;

            pts.tag = "Level";
            Destroy(spawnPoint);
        }
        Destroy(gameObject);
        playerCon.HandleLevel();
    }

}
