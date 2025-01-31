using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximal hälsa
    [HideInInspector]
    public int currentHealth; // Nuvarande hälsa

    private void Start()
    {
        currentHealth = maxHealth; // Initiera hälsan för varje fiende separat
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Minska hälsan för endast denna fiende
        Debug.Log($"{gameObject.name} took {damage} damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        Destroy(gameObject); // Förstör endast denna fiende
    }
}
