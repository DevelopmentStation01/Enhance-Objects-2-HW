using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRateManager : MonoBehaviour
{
    [SerializeField] private float maxSpawnRate = 1f; // Max Spawn Rate threshold
    [SerializeField] private float enemySpawnRateStep = 100f; // The Spawn Rate will be increased every 100 points

    private GameManager gameManager;
    private ScoreManager scoreManager;

    private int currentLevel = 0;

    public int CurrentLevel { get { return currentLevel; } }

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        scoreManager = gameManager.scoreManager;

        // Subscribe to the score update event
        scoreManager.OnScoreUpdated.AddListener(UpdateSpawnRate);
    }

    private void UpdateSpawnRate()
    {
        // Calculate the new level based on the score
        int newLevel = Mathf.FloorToInt(scoreManager.GetScore() / enemySpawnRateStep);

        // Check if the level needs to be incremented
        if (newLevel > currentLevel)
        {
            currentLevel = newLevel;

            // Increase the spawn rate by 0.1 for every level increase
            float newSpawnRate = gameManager.EnemySpawnRate + 0.1f;
            
            // Make sure the new spawn rate does not exceed the maximum spawn rate
            gameManager.EnemySpawnRate = Mathf.Min(newSpawnRate, maxSpawnRate);

            // You can add any additional logic related to level progression here
            Debug.Log($"<color=green>Level increased to {currentLevel}, EnemySpawnRate increased to {gameManager.EnemySpawnRate}</color>");
        }
    }
}
