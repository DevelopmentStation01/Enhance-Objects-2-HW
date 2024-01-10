using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnener : MonoBehaviour
{
    [SerializeField] private PickUpSpawn[] pickups;

    [Range(0, 1)] [SerializeField] private float pickupProbability;
    List<Pickup> pickupPool = new List<Pickup>();
    Pickup selectPickup;
    
    
    private void Start()
    {
        foreach (PickUpSpawn spawn in pickups)
        {
            for ( int i = 0; i < spawn.spawnWeight; i++)
            {
                pickupPool.Add(spawn.pickup);
            }
        }
    }

    public void SpawnPickup(Vector2 position)
    {
        if (pickupPool.Count <= 0)
        {
            return;
        }

        if (Random.Range(0.0f, 1.0f) < pickupProbability)
        {
            selectPickup = pickupPool[Random.Range(0, pickupPool.Count)];
            Instantiate(selectPickup, position, Quaternion.identity);
        }
    }
}

[System.Serializable]
public struct PickUpSpawn
{
    public Pickup pickup;
    public int spawnWeight;
}
