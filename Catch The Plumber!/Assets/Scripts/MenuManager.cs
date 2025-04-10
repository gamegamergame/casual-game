using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text highscoresText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowHighscores();
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
