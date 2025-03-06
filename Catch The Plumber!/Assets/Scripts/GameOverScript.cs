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

        lives--;
        StartCoroutine(SpawnDelay());

    }

    IEnumerator SpawnDelay()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        CapsuleCollider2D plumberCollider = plumber.GetComponent<CapsuleCollider2D>();

        plumber.gravityScale = 0;
        plumber.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        plumber.linearVelocity = Vector2.zero;
        plumber.angularVelocity = 0;
        plumberCollider.enabled = false;

        yield return new WaitForSecondsRealtime(2);

        plumberCollider.enabled = true;
        plumber.gravityScale = 1;


        //yield on a new YieldInstruction that waits for 5 seconds.

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);


    }
}
