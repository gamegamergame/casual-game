using UnityEngine;

public class PlumberScript : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    float speed;

    float timer = 3;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //Debug.Log(timer);

        if (timer < 0)
        {
            rb.AddForce(new Vector2(Random.Range(-5,5) * speed, Random.Range(-5, 5) * speed));

            timer = 5;
        }
    }
}
