using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D plumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D()
    {
        plumber.transform.position = new Vector2(0,0);
        plumber.linearVelocity = Vector2.zero;
    }
}
