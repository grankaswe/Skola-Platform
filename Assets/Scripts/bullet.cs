using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1; // Skadan kulan orsakar

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kolla om kulan träffar en fiende
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Skada endast den träffade fienden
            Debug.Log($"Bullet hit {collision.name}. Enemy health: {enemy.currentHealth}");
        }

        // Förstör kulan vid träff
        Destroy(gameObject);
    }
}
