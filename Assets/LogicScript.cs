using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int highScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public AudioSource scoreAudioSource;

    private bool isPaused = false;
    private bool gameStarted = false;

    void Start()
    {
        // Initially hide the game over and pause screens
        gameOverScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f; // Ensure the game is running at full speed

        // Initialize high score and game state
        gameStarted = true;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (gameStarted)
        {
            // Check for Escape key press to toggle pause
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }

            // Allow pressing Space to resume from pause menu
            if (isPaused && Input.GetKeyDown(KeyCode.Space))
            {
                ResumeGame();
            }

            // Restart game if game over screen is active and Space is pressed
            if (gameOverScreen.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                restartGame();
            }
        }
    }

    public void addScore()
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString();
        if (scoreAudioSource != null) scoreAudioSource.Play();
    }

    public void UpdateHighScore()
    {
        if (playerScore > highScore)
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log("New high score saved: " + highScore);
        }
    }

    public void gameOver()
    {
        UpdateHighScore();  // Update high score before showing game over screen
        gameOverScreen.SetActive(true);
        isPaused = false;  // Allow player to decide to restart
    }

    public void restartGame()
    {
        playerScore = 0;  // Reset score
        scoreText.text = playerScore.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the scene
    }

    public void PauseGame()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
