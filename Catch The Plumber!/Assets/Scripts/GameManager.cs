using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D plumberRB;

    [SerializeField]
    PlumberScript plumberScript;

    [SerializeField]
    HighscoresList highscoreList;

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

    public float Distance
    {
        get { return distance; }
        set { distance = value; }
    }

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

    public bool isSpawning;

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


        //starts as game paused
        Time.timeScale = 0f;


        //isSpawning = true;
        //StartCoroutine(SpawnDelay());
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
            AddNewScore(distance);
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
            //plumberRB.transform.position = new Vector2(camPos.position.x, camPos.position.y);

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

    public IEnumerator OnHPLoss()
    {
        CapsuleCollider2D plumberCollider = plumberRB.GetComponent<CapsuleCollider2D>();

        isSpawning = true;


        //yield return new WaitForSecondsRealtime(0.5f);

        plumberRB.AddForceY(1000f);
        //plumberRB.AddForceX(1000f);




        yield return new WaitForSecondsRealtime(1.5f);

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

    //Saves highscores to JSON
    public static void SaveHighscores(HighscoresList data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Highscores", json);
        PlayerPrefs.Save();
    }

    //Loads highscores from JSON
    public static HighscoresList LoadHighscores()
    {
        string json = PlayerPrefs.GetString("Highscores", "");
        if (string.IsNullOrEmpty(json))
            return new HighscoresList(); // return empty list if nothing saved

        return JsonUtility.FromJson<HighscoresList>(json);
    }

    //For adding new scores to the top 5 list
    public void AddNewScore(float newScore)
    {
        HighscoresList data = LoadHighscores();

        // Add new score
        data.scores.Add(newScore);

        // Sort descending
        data.scores = data.scores.OrderByDescending(score => score).ToList();

        // Keep top 5
        if (data.scores.Count > 5)
            data.scores = data.scores.Take(5).ToList();

        SaveHighscores(data);
    }

}
