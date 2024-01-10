using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONExample : MonoBehaviour
{
    void Start()
    {
        SampleData sampleData = new SampleData();
        sampleData.name = "Mike";
        sampleData.score = 15.0f;

        string data = JsonUtility.ToJson(sampleData);

        Debug.Log($"JSON = {data}");

        string filePath = Path.Combine(Application.dataPath, "JSONFolder/sampleJSON.json");
        File.WriteAllText(filePath, data);
    }
}
