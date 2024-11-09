using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // This function will be called when the Start button is clicked
    public void StartGame()
    {
        // Find and destroy the BackgroundMusic GameObject to stop the music
        GameObject backgroundMusic = GameObject.Find("BackgroundMusic");
        if (backgroundMusic != null)
        {
            Destroy(backgroundMusic); // This will stop the music
        }

        // Load the main game scene (replace "GameWorldScene" with your actual scene name)
        SceneManager.LoadScene("GameWorldScene");
    }

    // Detect spacebar press to start game from the main menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    // This function will be called when the Exit button is clicked
    public void ExitGame()
    {
        Debug.Log("Game is exiting"); // This line will show in the console during testing
        Application.Quit();
    }
}
