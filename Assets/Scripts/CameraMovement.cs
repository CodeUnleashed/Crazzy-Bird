using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    // The horizontal speed
    public int HorizontalSpeed;

    // Whether or not to stop the movement
    private static bool stop;

    // The terrain generator
    public TerrainGenerator Generator;

    void Start() { stop = false; }  // Indicating not to stop
    
    void FixedUpdate()
    {
        // Checking to see if stop
        if (stop)
            return;

        // Getting the position
        Vector3 newPosition = transform.position;

        // Moving
        newPosition.x += HorizontalSpeed * Time.deltaTime;

        // Setting the position
        transform.position = newPosition;
    }

    public void Stop()
    {
        stop = true;    // Stopping the simulation
    }
}