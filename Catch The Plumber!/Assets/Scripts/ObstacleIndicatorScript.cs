using UnityEngine;

public class ObstacleIndicatorScript : MonoBehaviour
{

    SpriteRenderer sr;

    Camera cam;

    [SerializeField]
    Transform obstaclePos;

    float camXPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        camXPos = cam.transform.position.x + 13f;

        //Debug.Log(camXPos);
        //Debug.Log(obstaclePos.position.x);
        transform.position = new Vector2(camXPos, transform.position.y);


        //after obstacle has come into view of the camera
        if (obstaclePos.position.x <= camXPos)
        {
            sr.enabled = false;
        }
    }
}