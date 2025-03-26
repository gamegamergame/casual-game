using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D plumberRB;

    [SerializeField]
    PlumberScript plumberScript;

    [SerializeField]
    Transform playerPos;

    int lives = 3;

    [SerializeField]
    TMP_Text livesText;

    [SerializeField]
    TMP_Text distanceText;

    bool isSpawning;

    [SerializeField]
    SpriteRenderer plumberIndicator;

    [SerializeField]
    Transform camPos;

    [SerializeField]
    BoxCollider2D floor;

    bool isGamePaused;

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plumberIndicator.enabled = false;

        //cam = Camera.main;

        isSpawning = true;
        StartCoroutine(SpawnDelay());

        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + lives;
        distanceText.text = "Distance: " + Vector2.Distance(Vector2.zero, plumberRB.transform.position);

        if (lives <= 0)
        {
            SceneManager.LoadScene(1);
        }

        //if (playerPos.position.x < Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect))
        //{
        //lives -= 100;
        //}

        if (plumberRB.transform.position.y > plumberIndicator.transform.position.y)
        {
            plumberIndicator.enabled = true;
            plumberIndicator.transform.position = new Vector2(plumberRB.transform.position.x, plumberIndicator.transform.position.y);
        }
        else
        {
            plumberIndicator.enabled = false;
        }

        //make sure the plumber follows the camera as he is spawning
        if (isSpawning)
        {
            plumberScript.currentState = PlumberScript.plumberStates.Spawning;
            plumberRB.transform.position = new Vector2(camPos.position.x, camPos.position.y);
        }
    }

    //void OnTriggerEnter2D()
    //{
    //float distance = Vector2.Distance(plumber.position, playerPos.position);

    //lives--;
    //StartCoroutine(SpawnDelay());
    //}

    /// <summary>
    /// respawns plumber in the center of the screen stops all motion and deactivates collider after 2 seconds everything goes back to normal
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnDelay()
    {
        CapsuleCollider2D plumberCollider = plumberRB.GetComponent<CapsuleCollider2D>();

        plumberRB.gravityScale = 0;
        plumberRB.linearVelocity = Vector2.zero;
        plumberRB.angularVelocity = 0;
        plumberRB.rotation = 0;

        isSpawning = true;

        plumberCollider.enabled = false;

        yield return new WaitForSecondsRealtime(2);

        plumberCollider.enabled = true;
        plumberRB.gravityScale = 1;

        yield return new WaitForSecondsRealtime(0.5f);

        isSpawning = false;
    }
}
