using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 lastMousePosition;

    [Header("Camera Movement Settings")]
    public float panSpeed = 2.0f;
    public float maxX = 10.0f;
    public float maxY = 10.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * panSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + move;

            // Clamp the camera's position within the specified square area.
            newPosition.x = Mathf.Clamp(newPosition.x, -maxX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, -maxY, maxY);

            transform.position = newPosition;

            lastMousePosition = Input.mousePosition;
        }
    }
}
