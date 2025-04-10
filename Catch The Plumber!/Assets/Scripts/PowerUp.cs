using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float powerLength = 5f;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    PlumberScript plumberScript;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        plumberScript = FindObjectOfType<PlumberScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Plumber"))
        {
            ApplyEffect(collision.gameObject);
            Destroy(gameObject); // Remove power-up after use
        }
    }

    private void ApplyEffect(GameObject plumber)
    {
        Rigidbody2D rb = plumber.GetComponent<Rigidbody2D>();
        SpriteRenderer plumberRenderer = plumber.GetComponent<SpriteRenderer>(); // Get SpriteRenderer for color change
        Color powerUpColor = GetComponent<SpriteRenderer>().color; // Get color of the power-up
        if (rb == null || plumberRenderer == null) return;

        if (CompareTag("LessBounce")) // Check if the power-up is tagged "LessBounce"
        {
            // Change the Plumber's color to match the power-up's color
            //StartCoroutine(ReducedBounciness(plumber, plumberRenderer, powerUpColor, rb));
        }

        if (CompareTag("DistanceBoost"))
        {
            gameManager.BonusDistance += 50;
        }

        if (CompareTag("ExtraBounce"))
        {
            plumberScript.extraBounces = 5;
            rb.sharedMaterial.bounciness = 1.2f;
            plumberRenderer.color = powerUpColor;
        }
    }
}