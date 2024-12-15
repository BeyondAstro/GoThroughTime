using UnityEngine;

public class CameraControl3DLERP : MonoBehaviour
{
    public Transform[] targets; // Array of target positions
    public float smoothSpeed = 10f; // Speed of the camera movement
    public Vector3 offset; // Offset to maintain relative to the target

    private int currentTargetIndex = 0; // Index of the current target

    void Update()
    {
        // Check for space bar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Move to the next target
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
    }

    void FixedUpdate()
    {
        if (targets.Length == 0) return; // Ensure there are targets to move between

        // Get the desired position based on the current target and offset
        Vector3 desiredPosition = targets[currentTargetIndex].position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        // Look at the current target
        transform.LookAt(targets[currentTargetIndex]);
    }
}