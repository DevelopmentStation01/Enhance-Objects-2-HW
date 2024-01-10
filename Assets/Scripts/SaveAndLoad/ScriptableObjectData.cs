using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectData : MonoBehaviour
{
    public ScriptableObjectSample sample;

    private void Start()
    {
        if (!sample)
            return;

        Debug.Log($"Loaded file with name = {sample.name} with a score {sample.score} starting at {sample.startPosition}");
    }
}
