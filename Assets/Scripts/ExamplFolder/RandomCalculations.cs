using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomCalculations : MonoBehaviour
{
    public float hight;
    public float lenght;

    public float area;

    public static int randNum = 0;

    void Start()
    {
        CalculateRandNum();
        CalculateArea();
    }

    public static void CalculateRandNum()
    {
        randNum++;
        Debug.Log($"Random Number = {randNum}");
    }

    void CalculateArea()
    {
        area = lenght * hight;
        Debug.Log($"Area = {area}");
    }

    public void RandomMethod()
    {
        area = lenght * hight;
    }
}
