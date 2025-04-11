using UnityEngine;

public class PlumberScript : MonoBehaviour
{

    Rigidbody2D rb;

    CapsuleCollider2D plumberCollider;


    [SerializeField]
    Vector2 centerOfMass;

    [SerializeField]
    float runSpeed;  
    
    [SerializeField]
    float standSpeed;

    [SerializeField]
    float jumpSpeed;

    float timer = 1f;

    [SerializeField]
    float shakeIntensity = 0.05f;

    SpriteRenderer plumberRenderer;

    public int extraBounces = 0;

    public enum plumberStates
    {
        Running,
        Standing,
        Jumping,
        Spawning
    }
    public plumberStates currentState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plumberCollider = GetComponent<CapsuleCollider2D>();
        plumberRenderer = GetComponent<SpriteRenderer>();
        currentState = plumberStates.Spawning;

        rb.centerOfMass = centerOfMass;

    }

    // Update is called once per frame
    void Update()
    {


        //shaking animation before jump
        if (timer < 1f) //Shake only in the last second
        {
            float shakeOffset = Mathf.Sin(Time.time * 100) * shakeIntensity;
            transform.position += new Vector3(shakeOffset, 0, 0);
        }


        //check if plumber is on the platform in order to jump
        if (plumberCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            timer -= Time.deltaTime;

            //currentState = plumberStates.Standing;

            //if on platform after 1.5 seconds switch to jump state
            if (timer < 0)
            {
                currentState = plumberStates.Jumping;

                timer = 1.5f;
            }
        }
        else
        {
            timer = 1.5f;
        }

        //adds a constant force towards the right
        if (currentState == plumberStates.Running)
        {
            rb.AddForce(new Vector2(runSpeed, 0));
        }

        //balances out the plumber 
        else if (currentState == plumberStates.Standing)
        {
            /*if (transform.rotation.z > 1)
            {
                //rb.AddTorque(standSpeed);
            }
            else if (transform.rotation.z < 1)
            {
                //rb.AddTorque(standSpeed);
            }
            else
            {
                currentState = plumberStates.Jumping;
            }*/
        }

        //adds a constant force towards the right
        else if (currentState == plumberStates.Jumping)
        {
            rb.AddForce(new Vector2(jumpSpeed, jumpSpeed));
            currentState = plumberStates.Running;
        }

        else if (currentState == plumberStates.Spawning)
        {

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (extraBounces > 0 && (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Obstacle")))
        {
            extraBounces--;
            if (extraBounces <= 0 && rb.sharedMaterial != null)
            {
                rb.sharedMaterial.bounciness = 0.6f;
                plumberRenderer.color = Color.white;
            }
        }
    }
}