using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    private float topBoundary = 12.5f;
    private float bottomBoundary = -11.5f;
    
    private Animator animator; // Animator for wing flap

    // AudioSource components for different sound effects
    public AudioSource flapAudioSource;     // For flap sound
    
    public AudioSource gameOverAudioSource; // For game over sound

    void Start()
    {
        // Initialize logic and animator components
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on Bird object.");
        }

        // Check if audio sources are assigned
        if (flapAudioSource == null)
        {
            Debug.LogError("Flap audio source is not assigned.");
        }
        if (gameOverAudioSource == null)
        {
            Debug.LogError("Game Over audio source is not assigned.");
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) && birdIsAlive)
        {
            Flap();
        }
    
        OutOfScreen();
    }

    private void Flap()
    {
        myRigidBody.velocity = Vector2.up * flapStrength;
        
        if (flapAudioSource != null)
        {
            flapAudioSource.Play();
            Debug.Log("Flap sound played.");
        }
        
        animator.SetTrigger("Flap");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive)
        {
            if (gameOverAudioSource != null)
            {
                gameOverAudioSource.Play();
                Debug.Log("Game Over sound played.");
            }

            logic.gameOver();
            birdIsAlive = false;
        }
    }

    private void OutOfScreen()
    {
        if (birdIsAlive && (myRigidBody.position.y > topBoundary || myRigidBody.position.y < bottomBoundary))
        {
            if (gameOverAudioSource != null)
            {
                gameOverAudioSource.Play();
                Debug.Log("Game Over sound played due to out-of-bounds.");
            }
            
            logic.gameOver();
            birdIsAlive = false;
        }
    }
}
