using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float leftBoundary = 2f; // Distance from left edge before camera moves
    public float rightBoundary = 2f; // Distance from right edge before camera moves
    public float smoothSpeed = 5f;

    private float halfScreenWidth;

    void Start()
    {
        halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect; // Half the screen width in world units
    }

    void LateUpdate()
    {
        if (player == null) return;

        float cameraLeft = transform.position.x - halfScreenWidth;
        float cameraRight = transform.position.x + halfScreenWidth;
        float playerX = player.position.x;

        Vector3 targetPosition = transform.position;

        if (playerX < cameraLeft + leftBoundary)
        {
            targetPosition.x = playerX - leftBoundary + halfScreenWidth;
        }
        else if (playerX > cameraRight - rightBoundary)
        {
            targetPosition.x = playerX + rightBoundary - halfScreenWidth;
        }

        targetPosition.y = transform.position.y; // Keep Y position unchanged

        // Smoothly move camera towards target
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}