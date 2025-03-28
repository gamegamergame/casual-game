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
        //after obstacle has come into view of the camera
        if (cam.transform.position.x + 10.5f >= transform.position.x)
        {
            sr.enabled = false;
        }
    }
}
