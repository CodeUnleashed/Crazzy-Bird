using UnityEngine;
using System.Collections;

public class IntroBirdMovement : MonoBehaviour {
    public float VerticalRange, VerticalSpeed, RotationSpeed;

    private Vector3 velocity;

    void Start()
    {
        velocity = transform.position;
    }

    void FixedUpdate()
    {
        // Moviing the bird vertically
        Vector3 newPosition = transform.position;

        if (newPosition.y <= velocity.y - VerticalRange || newPosition.y >= velocity.y + VerticalRange)
            VerticalSpeed = -VerticalSpeed;

        newPosition.y += VerticalSpeed * Time.deltaTime;

        transform.position = newPosition;
    }
}
