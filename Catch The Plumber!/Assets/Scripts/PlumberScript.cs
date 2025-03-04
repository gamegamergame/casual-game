using UnityEngine;

public class PlumberScript : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    float speed;

    float timer = 5;

    [SerializeField]
    float shakeIntensity = 0.05f;


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

        //shaking animation before jump
        if (timer < 1f) //Shake only in the last second
        {
            float shakeOffset = Mathf.Sin(Time.time * 100) * shakeIntensity;
            transform.position += new Vector3(shakeOffset, 0, 0);
        }

        if (timer < 0)
        {
            rb.AddForce(new Vector2(Random.Range(1,5) * speed, Random.Range(1, 5) * speed));

            timer = 5;
        }
    }
}
