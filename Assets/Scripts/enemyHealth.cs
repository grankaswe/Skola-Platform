using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximal h�lsa
    [HideInInspector]
    public int currentHealth; // Nuvarande h�lsa

    private void Start()
    {
        currentHealth = maxHealth; // Initiera h�lsan f�r varje fiende separat
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Minska h�lsan f�r endast denna fiende
        Debug.Log($"{gameObject.name} took {damage} damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        Destroy(gameObject); // F�rst�r endast denna fiende
    }
}
