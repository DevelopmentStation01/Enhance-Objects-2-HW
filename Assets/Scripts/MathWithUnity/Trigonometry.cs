using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigonometry : MonoBehaviour
{
    public Transform obj;

    public Vector2 startPos, amplitude, frequency;


    private void Start()
    {
        
    }

    private void Update()
    {
        // Sine();
        Elipse();
    }

    private void Sine()
    {
        float xPos = startPos.x + Time.timeSinceLevelLoad;
        float yPos = startPos.y + amplitude.y * Mathf.Sin(frequency.y * Time.timeSinceLevelLoad); // Cos - cosinus, Tan - tangens

        obj.position = new Vector2(xPos, yPos);
    }

    private void Elipse()
    {
        float xPos = startPos.x + amplitude.x * Mathf.Sin(frequency.x * Time.timeSinceLevelLoad);
        float yPos = startPos.y + amplitude.y * Mathf.Cos(frequency.y * Time.timeSinceLevelLoad);

        obj.position = new Vector2(xPos, yPos);
    }
}
