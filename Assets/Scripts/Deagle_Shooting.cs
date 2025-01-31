using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; // The point from where bullets will spawn
    public GameObject bulletPrefab; // Prefab for the bullet
    public float bulletSpeed = 20f; // Speed of the bullet
    public float fireRate = 2f; // Shots per second

    public GameObject crosshair; // Prefab for the crosshair
    public LineRenderer laserLine; // LineRenderer for laser sight

    private Camera mainCamera; // Reference to the main camera
    private float lastShotTime; // Tracks the last time a shot was fired

    private void Start()
    {
        mainCamera = Camera.main;

        // Hide laser and crosshair initially
        if (laserLine) laserLine.enabled = false;
        if (crosshair) crosshair.SetActive(false);
    }

    private void Update()
    {
        // Rotate FirePoint to face the mouse
        Aim();

        // Handle shooting input
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShotTime + 1f / fireRate)
        {
            Shoot();
            lastShotTime = Time.time; // Update the last shot time
        }
    }

    private void Aim()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure we're working in 2D

        // Calculate the direction vector
        Vector2 aimDirection = (mousePosition - firePoint.position).normalized;

        // Rotate the firePoint to face the mouse
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        // Update crosshair position
        if (crosshair)
        {
            crosshair.SetActive(true);
            crosshair.transform.position = mousePosition;
        }

        // Draw laser sight
        if (laserLine)
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, firePoint.position);
            laserLine.SetPosition(1, mousePosition);
        }
    }

    private void Shoot()
    {
        // Spawn the bullet at the firePoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        Debug.Log("Bullet fired!");
    }
}
