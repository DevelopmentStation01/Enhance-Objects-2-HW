using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : Pickup, IDamageable
{
    // Flag to track whether the pickup has already been collected
    private bool isCollected = false;

    // Object to use as a lock for synchronization
    private readonly object collectionLock = new object();

    public override void OnPicked()
    {
        // Use a lock to ensure thread-safe access to isCollected
        lock (collectionLock)
        {
            // Check if the pickup has already been collected
            if (isCollected)
            {
                return;
            }

            base.OnPicked();

            var player = GameManager.GetInstance().GetPlayer();
            player.nuke.AddNuke();

            isCollected = true;
        }
    }

    public void GetDamage(float damage)
    {
        OnPicked();
    }
}
