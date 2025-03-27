using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float baseScrollSpeed = 5f; // Base auto-scroll speed
    public float pushSpeed = 10f; // Additional speed when player is near the right edge
    public float rightBoundary = 2f; // Distance from right edge before pushing
    public float smoothSpeed = 5f; // Camera smoothing

    public Transform[] backgrounds; // Array of background elements
    public float backgroundWidth = 10f; // Width of each background segment

    private float halfScreenWidth;

    void Start()
    {
        halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect; // Half the screen width in world units
    }

    void LateUpdate()
    {
        if (player == null) return;

        float cameraRight = transform.position.x + halfScreenWidth;
        float playerX = player.position.x;

        Vector3 targetPosition = transform.position;

        // Base scrolling speed
        float currentSpeed = baseScrollSpeed;

        // If player is near the right side, increase speed
        if (playerX > cameraRight - rightBoundary)
        {
            float pushFactor = (playerX - (cameraRight - rightBoundary)) / rightBoundary;
            currentSpeed += pushFactor * pushSpeed;
        }

        // Move the camera
        targetPosition.x += currentSpeed * Time.deltaTime;
        targetPosition.y = transform.position.y; // Keep Y position unchanged

        // Smooth transition
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Handle background looping
        LoopBackgrounds();
    }

    void LoopBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Transform bg = backgrounds[i];

            // If background is fully behind the camera, reposition it forward
            if (bg.position.x < transform.position.x - halfScreenWidth - backgroundWidth)
            {
                float newX = bg.position.x + backgroundWidth * backgrounds.Length;
                bg.position = new Vector3(newX, bg.position.y, bg.position.z);
            }
        }
    }
}