using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
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

        // Set up the game state (do not handle main menu logic here)
        gameStarted = true;
    }

    void Update()
    {
        if (gameStarted)
        {
            // Check for Escape key press to toggle pause during gameplay
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }

            // Allow pressing Space to resume from pause menu as well
            if (isPaused && Input.GetKeyDown(KeyCode.Space))
            {
                ResumeGame();
            }

            // Wait for spacebar to restart the game if game is over
            if (gameOverScreen.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f; // Unfreeze the game
        gameOverScreen.SetActive(false); // Hide game over screen
    }

    public void addScore()
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString();
        if (scoreAudioSource != null)
        {
            scoreAudioSource.Play();
            Debug.Log("Score sound played.");
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the scene to restart
    }

    // Show the Game Over screen and wait for space to restart the game
    public void gameOver()
    {
        gameOverScreen.SetActive(true);  // Show game over screen
        isPaused = false;  // Do not pause the game, let it run while the player decides to restart
    }

    public void PauseGame()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;  // Pause the game time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;  // Resume the game
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;  // Ensure the game time is normal before switching scenes
        SceneManager.LoadScene("MainMenuScene");
    }

    // This method restarts the game (called when space is pressed after game over)
    private void RestartGame()
    {
        playerScore = 0;  // Reset score (optional)
        scoreText.text = playerScore.ToString();
        gameStarted = false;  // Reset gameStarted flag
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the scene to restart the game
    }
}
