using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D plumber;

    [SerializeField]
    Transform playerPos;

    int lives = 3;

    [SerializeField]
    TMP_Text livesText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            SceneManager.LoadScene(1);
        }

        if (playerPos.position.x < Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect))
        {
            //lives -= 100;
        }
    }

    void OnTriggerEnter2D()
    {
        //float distance = Vector2.Distance(plumber.position, playerPos.position);
        plumber.transform.position = new Vector2(playerPos.position.x, playerPos.position.y + 4);
        plumber.linearVelocity = Vector2.zero;
        lives--;
    }
}
