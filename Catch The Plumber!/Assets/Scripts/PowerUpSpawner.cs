using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs to spawn
    public Transform player;  // Reference to the player
    public float maxX;  // Define spawn boundaries
    public float spawnInterval = 2f; // Time between spawns

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    void SpawnObject()
    {
        // Get screen bounds in world space
        float minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;

        float randomX = Random.Range(player.position.x + 25, player.position.x + 25 + maxX);
        float randomY = Random.Range(minY + 1f, maxY);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        if (!IsOverlapping(spawnPosition))
        {
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            SpawnObject(); // Recursive call to try again
        }
    }

    bool IsOverlapping(Vector2 position)
    {
        // Perform a 2D overlap check (adjust size depending on your object)
        Collider2D overlap = Physics2D.OverlapCircle(position, 0.5f);
        return overlap != null;
    }
}