using UnityEngine;

public class PlumberScript : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
