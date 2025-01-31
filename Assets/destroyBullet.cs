using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Optional: Add logic to ignore specific objects
        if (collision.gameObject.CompareTag("Player"))
        {
            return; // Don't destroy the bullet if it hits the player
        }

        // Destroy this bullet on impact
        Destroy(gameObject);
    }
}
