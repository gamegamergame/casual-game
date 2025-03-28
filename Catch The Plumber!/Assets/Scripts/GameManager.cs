using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D plumberRB;

    [SerializeField]
    PlumberScript plumberScript;

    [SerializeField]
    Transform playerPos;

    [SerializeField]
    Transform ObstacleIndicatorSpawn;

    int lives = 3;

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    float distance;

    float bonusDistance = 0;

    public float BonusDistance
    {
        get { return bonusDistance; }
        set { bonusDistance = value; }
    }

    [SerializeField]
    List<SpriteRenderer> heartUI = new List<SpriteRenderer>();

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
    SpriteRenderer plumberSpriteRenderer;

    private bool isFlashing;

    //bool isGamePaused;

    [SerializeField]
    public List<GameObject> obstacleList = new List<GameObject>();

    [SerializeField]
    GameObject obstacleIndicator;

    public List<GameObject> ObstacleList
    {
        get { return obstacleList; }
        set { obstacleList = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plumberIndicator.enabled = false;

        //cam = Camera.main;

        isSpawning = true;
        StartCoroutine(SpawnDelay());

        //isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(Vector2.zero, plumberRB.transform.position) + BonusDistance;
        livesText.text = "Lives: " + lives;
        distanceText.text = "Distance: " + distance.ToString("0");

        //update hp loss
        if (lives == 2)
        {
            heartUI[2].color = Color.gray;
        }
        else if (lives == 1) 
        {
            heartUI[1].color = Color.gray;
        }
        else if (lives <= 0)
        {
            heartUI[0].color = Color.gray;
            SceneManager.LoadScene(1);
        }


        foreach (GameObject obstacle in obstacleList) 
        { 
            Instantiate(obstacleIndicator, new Vector2(ObstacleIndicatorSpawn.position.x - 5, obstacle.transform.position.y) , Quaternion.identity);
        }

        //turn on the indicator if the plumber is offscreen
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

            // Start the flashing effect when spawning
            if (!isFlashing)
            {
                StartCoroutine(FlashDuringSpawning());
            }
        }
    }

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

        yield return new WaitForSecondsRealtime(1);

        plumberCollider.enabled = true;
        plumberRB.gravityScale = 1;

        yield return new WaitForSecondsRealtime(0.5f);

        // Stop the flashing once spawning is complete
        isSpawning = false;
        isFlashing = false;
        StopCoroutine(FlashDuringSpawning());
        ResetPlumberOpacity();
    }

    // Coroutine to handle flashing effect during spawning
    private IEnumerator FlashDuringSpawning()
    {
        isFlashing = true;

        float flashDuration = 0.2f; // Interval between flashes
        float nextFlashTime = Time.time + flashDuration;

        while (isSpawning)
        {
            // Toggle opacity between 0 and 1 to create the flashing effect
            if (Time.time >= nextFlashTime)
            {
                Color currentColor = plumberSpriteRenderer.color;
                plumberSpriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a == 1f ? 0.7f : 1f);
                nextFlashTime = Time.time + flashDuration;
            }

            yield return null;
        }
    }

    // Reset opacity to normal when the flashing stops
    private void ResetPlumberOpacity()
    {
        Color currentColor = plumberSpriteRenderer.color;
        plumberSpriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
    }
}
