using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Referens till spelarens transform
    public float speed = 8f; // Fiendens r�relsehastighet
    public float followRadius = 10f; // Radien inom vilken fienden b�rjar f�lja spelaren

    private Rigidbody2D rb;
    private bool isFollowing = false; // Om fienden aktivt f�ljer spelaren
    private bool isFacingRight = true; // Om fienden �r v�nd �t h�ger

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned! Assign it in the Inspector.");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Kolla om spelaren �r inom radien och b�rja f�lja om det inte redan g�rs
        if (!isFollowing && Vector2.Distance(transform.position, player.position) <= followRadius)
        {
            isFollowing = true; // Starta f�rf�ljelse
        }

        // Om fienden f�ljer spelaren, r�r sig bara l�ngs x-axeln
        if (isFollowing)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y); // H�ll y konstant
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            // Flytta fienden
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            // Kontrollera och hantera fiendens riktning
            if ((direction.x > 0 && isFacingRight) || (direction.x < 0 && !isFacingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        // Byt riktning
        isFacingRight = !isFacingRight;

        // Spegelv�nd fiendens sprite genom att skala x negativt
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        // Rita en cirkel i scenen f�r att visualisera f�ljeradien
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
