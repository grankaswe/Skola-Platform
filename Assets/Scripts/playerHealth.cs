using UnityEngine;
using UnityEngine.SceneManagement; // För att ladda scenen

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // Maximal hälsa
    private int currentHealth; // Nuvarande hälsa

    public float damageTimeout = 2f; // Tidsgräns för kontakt med fiende
    private float damageTimer = 0f; // Timer för att räkna tiden

    private bool isTakingDamage = false; // Håller koll på om spelaren tar skada

    public GameObject redScreen; // Röd skärmseffekt
    private SpriteRenderer redScreenRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        redScreenRenderer = redScreen.GetComponent<SpriteRenderer>();
        redScreenRenderer.color = new Color(1f, 0f, 0f, 0f); // Gör skärmen osynlig vid start
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            damageTimer += Time.deltaTime; // Tillägg tiden
            float alpha = Mathf.Min(damageTimer / damageTimeout, 1f); // Öka den röda effekten över tid
            redScreenRenderer.color = new Color(1f, 0f, 0f, alpha); // Gör skärmen mer röd
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        isTakingDamage = true; // Spelaren tar skada
        damageTimer = 0f; // Återställ timer

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Ladda huvudmenyn (Scene 0)
        SceneManager.LoadScene(0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Om spelaren är i kontakt med fienden i mer än 2 sekunder, ta skada
        if (collision.CompareTag("Enemy"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageTimeout)
            {
                TakeDamage(1); // Ta skada när kontakt varit längre än 2 sekunder
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Om spelaren inte längre är i kontakt med fienden, sluta ta skada
        if (collision.CompareTag("Enemy"))
        {
            damageTimer = 0f; // Återställ timer när spelaren lämnar fiendens område
            isTakingDamage = false;
            redScreenRenderer.color = new Color(1f, 0f, 0f, 0f); // Gör skärmen osynlig igen
        }
    }
}
