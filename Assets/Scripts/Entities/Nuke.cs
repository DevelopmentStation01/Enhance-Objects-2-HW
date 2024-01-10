using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke
{
    private readonly int maxNukes = 5;

    private int numberOfNukes;

    public Action<int> OnNuke;


    public Nuke (int _numberOfNukes = 0)
    {
        this.numberOfNukes = _numberOfNukes;
    }

    public void AddNuke()
    {
        if (numberOfNukes < maxNukes)
            numberOfNukes++;

        OnNuke?.Invoke(numberOfNukes);
    }

    public void UseNuke()
    {
        if (numberOfNukes > 0)
        {
            numberOfNukes--; // Reduce the number of nukes when used
            DestroyAllEntitiesExceptPlayer();
        }
        OnNuke?.Invoke(numberOfNukes);
    }

    private void DestroyAllEntitiesExceptPlayer()
    {
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject entity in entities)
        {
            MonoBehaviour.Destroy(entity);
        }
    }
}
