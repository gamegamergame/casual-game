using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
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
        if (rb == null || plumberRenderer == null) return;

        if (CompareTag("LessBounce")) // Check if the power-up is tagged "LessBounce"
        {
            // Change the Plumber's color to match the power-up's color
            Color powerUpColor = GetComponent<SpriteRenderer>().color; // Get color of the power-up
            StartCoroutine(ReducedBounciness(plumber, plumberRenderer, powerUpColor, rb));
        }
    }

    private IEnumerator ReducedBounciness(GameObject plumber, SpriteRenderer plumberRenderer, Color powerUpColor, Rigidbody2D rb)
    {
        float originalBounciness = rb.sharedMaterial.bounciness;
        rb.sharedMaterial.bounciness = 0f; // Set lower
        plumberRenderer.color = powerUpColor; // Change plumber to powerup color
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        plumberRenderer.color = Color.white; // Reset to original color
        rb.sharedMaterial.bounciness = 0.7f; // Reset to normal
    }
}