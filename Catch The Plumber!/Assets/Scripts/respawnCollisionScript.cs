using TMPro;
using UnityEngine;

public class respawnCollisionScript : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    void OnTriggerEnter2D()
    {
        //float distance = Vector2.Distance(plumber.position, playerPos.position);
        gameManager.Lives--;
        StartCoroutine(gameManager.SpawnDelay());
    }
}
