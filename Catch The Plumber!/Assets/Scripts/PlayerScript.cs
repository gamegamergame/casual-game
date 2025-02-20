using System;
using System.Drawing;

//using System.Numerics;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    Camera cam;


    Rigidbody2D rb;

    [SerializeField]
    float speed;

    const float floorPos = -4; 
    const float cielingPos = 4.5f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //contraint the y so that the platform does not go offscreen
        if (transform.position.y < floorPos)
        {
            transform.position = new Vector3(transform.position.x, floorPos, transform.position.z);
        }
        if (transform.position.y > cielingPos)
        {
            transform.position = new Vector3(transform.position.x, cielingPos, transform.position.z);
        }
    }

    public void MovePlayer(Vector2 vector2)
    {


        rb.AddForce (vector2 * speed);
        //Debug.Log(vector2);
        //direction = newDirection.normalized;

        //if (direction != Vector3.zero)
        //{
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        //}
    }

    internal void MouseMovePlayer()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        // Get direction from player to mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Apply force in that direction
        rb.AddForce(direction * speed);
    }
}
