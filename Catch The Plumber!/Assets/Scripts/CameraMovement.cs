using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float baseScrollSpeed = 50f; // Base auto-scroll speed
    public float smoothSpeed = 5f; // Camera smoothing

    public Transform[] backgrounds; // Array of background elements
    public float backgroundWidth = 10f; // Width of each background segment

    private int distanceCheckpoint = 0; // Tracks the last 50-mark reached

    private float halfScreenWidth;

    [SerializeField]
    GameManager gameManager;

    void Start()
    {
        halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect; // Half the screen width in world units
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Check if we passed a new 50-point threshold
        int currentCheckpoint = Mathf.FloorToInt(gameManager.Distance / 50f);
        if (currentCheckpoint > distanceCheckpoint)
        {
            distanceCheckpoint = currentCheckpoint;
            baseScrollSpeed = Mathf.Min(baseScrollSpeed + 25f, 200f);
        }

        float cameraRight = transform.position.x + halfScreenWidth;
        float playerX = player.position.x;

        Vector3 targetPosition = transform.position;

        // Base scrolling speed
        float currentSpeed = baseScrollSpeed;

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