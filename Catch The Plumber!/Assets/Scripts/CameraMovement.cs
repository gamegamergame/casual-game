using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float leftBoundary = 2f; // Distance from left edge before camera moves
    public float rightBoundary = 2f; // Distance from right edge before camera moves
    public float smoothSpeed = 5f;
    public float scrollSpeed = 100f; // Constant auto-scroll speed
    public float maxCatchupSpeed = 200f; // Maximum speed to catch up if player moves too fast
    public float catchupThreshold = 5f; // Distance threshold before boosting speed

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

        // Calculate how far the player is ahead of the camera center
        float playerOffset = playerX - transform.position.x;

        // Adjust scroll speed dynamically
        float dynamicScrollSpeed = scrollSpeed;
        if (playerOffset > catchupThreshold)
        {
            // If the player gets too far ahead, increase scroll speed up to maxCatchupSpeed
            dynamicScrollSpeed = Mathf.Lerp(scrollSpeed, maxCatchupSpeed, (playerOffset - catchupThreshold) / catchupThreshold);
        }

        // Apply auto-scroll
        targetPosition.x += dynamicScrollSpeed * Time.deltaTime;

        if (playerX < cameraLeft + leftBoundary)
        {
            //targetPosition.x = playerX - leftBoundary + halfScreenWidth;
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