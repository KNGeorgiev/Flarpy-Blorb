using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text highScoreText; // Assign this in the Inspector

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void StartGame()
    {
        GameObject backgroundMusic = GameObject.Find("BackgroundMusic");
        if (backgroundMusic != null) Destroy(backgroundMusic);

        SceneManager.LoadScene("GameWorldScene"); // Replace with your actual game scene name
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
