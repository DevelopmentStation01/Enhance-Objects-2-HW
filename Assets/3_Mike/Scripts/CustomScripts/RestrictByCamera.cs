using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictByCamera : MonoBehaviour
{
    public Vector2 GetCameraBounds(Camera cam)
    {
        Vector2 cameraBounds = Vector2.zero;

        if (cam != null)
        {
            // Get the extents of the camera's viewport in world coordinates
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            // Calculate boundaries considering the camera's position
            cameraBounds.x = camWidth;
            cameraBounds.y = camHeight;
        }

        return cameraBounds;
    }
}
