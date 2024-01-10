using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Singletone
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private float enemySpawnRate;
    [SerializeField] private GameObject playerPrefab;


    private GameObject tempEnemy;
    private bool isPlaying = false;

    [HideInInspector] public bool isEnemySpawning;

    public ScoreManager scoreManager;
    public PickUpSpawnener pickUpSpawnener;

    public Action OnGameStart;
    public Action OnGameOver;

    //private Weapon meleeWeapon = new Weapon("Melee", 1, 0);

    private static GameManager instance;
    private Player player;

    public static GameManager GetInstance()
    {
        return instance;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // not 'this' destroys the instance of the script/class if we change to 'this.gameObject', it destroys the the game object itself with the attached script
            Destroy(this);
        }

        instance = this;
    }

    // Spawn multiple enemies from the provided enemy prefabs
    private void CreateEnemy()
    {
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            // Instantiate a random enemy prefab from the array
            tempEnemy = Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)]);
            // Place the enemy at a random spawn position among the available spawn points
            tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        }
        
        //tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
        //tempEnemy.GetComponent<MeleeEnemy>().SetMeleeEnemy(2, 0.25f);
    }

    IEnumerator EnemySpawner()
    {
        while(isEnemySpawning)
        {
            yield return new WaitForSeconds(5.0f / enemySpawnRate);
            CreateEnemy();
        }
    }

    public void NotifyDeath(Enemy enemy)
    {
        pickUpSpawnener.SpawnPickup(enemy.transform.position);
    }

    public void StartGame()
    {
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        player.OnDeath += StopGame;
        isPlaying = true;

        OnGameStart?.Invoke();
        StartCoroutine(GameStarter());
    }

    IEnumerator GameStarter()
    {
        yield return new WaitForSeconds(2.0f);

        isEnemySpawning = true;
        StartCoroutine(EnemySpawner());
    }

    public void StopGame()
    {
        scoreManager.SetHighScore();

        StartCoroutine(GameStopper());
    }

    IEnumerator GameStopper()
    {
        isEnemySpawning = false;
        yield return new WaitForSeconds(2.0f);
        isPlaying = false;

        // Delete all the enemies
        foreach (Enemy item in FindObjectsOfType(typeof(Enemy)))
        {
            Destroy(item.gameObject);
        }

        // Delete all pickups
        foreach (Pickup item in FindObjectsOfType(typeof(Pickup)))
        {
            Destroy(item.gameObject);
        }

        OnGameOver?.Invoke();
    }
}
