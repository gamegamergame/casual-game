using System.Collections;
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

    bool isSpawning = false;



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

        //make sure the plumber follows the camera as he is spawning
        if (isSpawning) 
        {
            plumber.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        }
    }

    void OnTriggerEnter2D()
    {
        //float distance = Vector2.Distance(plumber.position, playerPos.position);

        lives--;
        StartCoroutine(SpawnDelay());

    }

    /// <summary>
    /// respawns plumber in the center of the screen stops all motion and deactivates collider after 2 seconds everything goes back to normal
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnDelay()
    {
        CapsuleCollider2D plumberCollider = plumber.GetComponent<CapsuleCollider2D>();
        isSpawning = true;

        plumber.gravityScale = 0;
        plumber.linearVelocity = Vector2.zero;
        plumber.angularVelocity = 0;
        plumber.rotation = 0;

        plumberCollider.enabled = false;

        yield return new WaitForSecondsRealtime(2);

        plumberCollider.enabled = true;
        plumber.gravityScale = 1;

        isSpawning = false;
    }
}
