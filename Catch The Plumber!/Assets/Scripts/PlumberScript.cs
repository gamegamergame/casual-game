using UnityEngine;

public class PlumberScript : MonoBehaviour
{

    Rigidbody2D rb;

    CapsuleCollider2D plumberCollider;

    [SerializeField]
    float runSpeed;

    [SerializeField]
    float jumpSpeed;

    float timer = 1f;

    [SerializeField]
    float shakeIntensity = 0.05f;

    public enum plumberStates
    {
        Running,
        Jumping,
        Spawning
    }
    public plumberStates currentState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plumberCollider = GetComponent<CapsuleCollider2D>();
        currentState = plumberStates.Spawning;

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

        //adds a constant force towards the right
        else if (currentState == plumberStates.Jumping)
        {
            rb.AddForce(new Vector2(jumpSpeed, jumpSpeed));

            //changeToJumpState();
            currentState = plumberStates.Running;
        }

        else if (currentState == plumberStates.Spawning)
        {

        }
        //Debug.Log(currentState);
    }

    //void changeToJumpState()
    //{
    //}
}