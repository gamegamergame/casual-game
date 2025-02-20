using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D plumber;

    [SerializeField]
    Transform playerPos;

    int lives = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        float distance = Vector2.Distance(plumber.position, playerPos.position);
        plumber.transform.position = new Vector2(playerPos.position.x, playerPos.position.y + 4);
        plumber.linearVelocity = Vector2.zero;
        lives--;
    }
}
