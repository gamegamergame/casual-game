using UnityEngine;

public class ObstacleIndicatorScript : MonoBehaviour
{

    SpriteRenderer sr;

    Camera cam;

    Transform obstaclePos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        obstaclePos = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obstaclePos.position.x <= cam.transform.position.x + cam.pixelWidth/2)
        {
            sr.enabled = false;
        }
    }
}
