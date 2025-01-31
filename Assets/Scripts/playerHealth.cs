using UnityEngine;
using UnityEngine.SceneManagement; // F�r att ladda scenen

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5; // Maximal h�lsa
    private int currentHealth; // Nuvarande h�lsa

    public float damageTimeout = 2f; // Tidsgr�ns f�r kontakt med fiende
    private float damageTimer = 0f; // Timer f�r att r�kna tiden

    private bool isTakingDamage = false; // H�ller koll p� om spelaren tar skada

    public GameObject redScreen; // R�d sk�rmseffekt
    private SpriteRenderer redScreenRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        redScreenRenderer = redScreen.GetComponent<SpriteRenderer>();
        redScreenRenderer.color = new Color(1f, 0f, 0f, 0f); // G�r sk�rmen osynlig vid start
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            damageTimer += Time.deltaTime; // Till�gg tiden
            float alpha = Mathf.Min(damageTimer / damageTimeout, 1f); // �ka den r�da effekten �ver tid
            redScreenRenderer.color = new Color(1f, 0f, 0f, alpha); // G�r sk�rmen mer r�d
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        isTakingDamage = true; // Spelaren tar skada
        damageTimer = 0f; // �terst�ll timer

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
        // Om spelaren �r i kontakt med fienden i mer �n 2 sekunder, ta skada
        if (collision.CompareTag("Enemy"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageTimeout)
            {
                TakeDamage(1); // Ta skada n�r kontakt varit l�ngre �n 2 sekunder
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Om spelaren inte l�ngre �r i kontakt med fienden, sluta ta skada
        if (collision.CompareTag("Enemy"))
        {
            damageTimer = 0f; // �terst�ll timer n�r spelaren l�mnar fiendens omr�de
            isTakingDamage = false;
            redScreenRenderer.color = new Color(1f, 0f, 0f, 0f); // G�r sk�rmen osynlig igen
        }
    }
}
