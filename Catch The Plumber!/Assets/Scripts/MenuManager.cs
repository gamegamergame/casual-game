using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    TMP_Text highscoresText;

    [SerializeField]
    GameObject pauseButton;

    [SerializeField]
    GameObject exitButton;

    [SerializeField]
    GameObject instructionsButton;

    [SerializeField]
    GameObject closeButton;

    [SerializeField]
    GameObject instuctionsPanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowHighscores();

        //pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        gameManager.isSpawning = true;
        StartCoroutine(gameManager.SpawnDelay());
    }

    //pauses game
    public void OnPause()
    {
        if (Time.timeScale < 1.0f)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
    public void OnPauseMenu()
    {
        exitButton.SetActive(!exitButton.activeSelf);
        instructionsButton.SetActive(!instructionsButton.activeSelf);

        //if ()
        //{
        //}

    }public void OnInstructions()
    {
        instuctionsPanel.SetActive(true);
    }

    public void OnClose()
    {
        instuctionsPanel.SetActive(false);
    }

    //Highscore display at the end screen
    public void ShowHighscores()
    {
        HighscoresList data = GameManager.LoadHighscores();
        highscoresText.text = "HighScores:\n";

        foreach (int score in data.scores)
        {
            highscoresText.text += score.ToString() + "\n";
        }
    }
}
