using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorsAndScalars : MonoBehaviour
{
    public Vector2 position;
    public Vector3 rotationVector;

    public float scalar;
    public float rotation;

    public Transform obj;

    private void Start()
    {
        //obj.position = position;
        //obj.position = scalar * position;
    }

    private void Update()
    {
        //obj.rotation = Quaternion.Euler(rotationVector);
        obj.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
