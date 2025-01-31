using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Referens till spelarens transform
    public float speed = 8f; // Fiendens rörelsehastighet
    public float followRadius = 10f; // Radien inom vilken fienden börjar följa spelaren

    private Rigidbody2D rb;
    private bool isFollowing = false; // Om fienden aktivt följer spelaren
    private bool isFacingRight = true; // Om fienden är vänd åt höger

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

        // Kolla om spelaren är inom radien och börja följa om det inte redan görs
        if (!isFollowing && Vector2.Distance(transform.position, player.position) <= followRadius)
        {
            isFollowing = true; // Starta förföljelse
        }

        // Om fienden följer spelaren, rör sig bara längs x-axeln
        if (isFollowing)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y); // Håll y konstant
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

        // Spegelvänd fiendens sprite genom att skala x negativt
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        // Rita en cirkel i scenen för att visualisera följeradien
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
