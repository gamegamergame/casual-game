using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Assign your prefab in the Inspector
    public Transform player;  // Reference to the player
    public float maxX;  // Define spawn boundaries
    public float spawnInterval = 2f; // Time between spawns

    [SerializeField]
    float spawnReductionTimer = 5f;

    bool spawnBool = false;


    GameManager manager;


    void Start()
    {
        manager = new GameManager();

        //InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
        StartCoroutine("SpawnObject");

    }

    void Update()
    {
        if (!spawnBool) 
        {

            spawnBool = true;
        }

        spawnReductionTimer -= Time.deltaTime;

        if (spawnReductionTimer <= 0)
        {
            spawnInterval -= 0.5f;
            Debug.Log(spawnInterval);
            spawnReductionTimer = 5f;
        }
        if (spawnInterval <= 0)
        {
            spawnInterval = 1;
        }
    }

    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(spawnInterval);

        // Get screen bounds in world space
        float minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;

        float randomX = Random.Range(player.position.x + 25, player.position.x + 25 + maxX);
        float randomY = Random.Range(minY + 1f, maxY);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        
        manager.ObstacleList.Add(Instantiate(objectToSpawn, spawnPosition, Quaternion.identity));

        StartCoroutine("SpawnObject");

    }
}
