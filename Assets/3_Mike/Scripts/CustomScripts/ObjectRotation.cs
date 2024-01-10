using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45;
    [SerializeField] private Vector3 rotationDirection = Vector3.forward;

    private void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime);
    }
}
