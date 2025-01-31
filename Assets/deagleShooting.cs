using UnityEngine;

public class DeagleShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the Deagle bullet prefab
    public Transform firePoint; // The point where the bullet is spawned
    public float bulletSpeed = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        // Optional: Set the bullet layer or tag here
        bullet.tag = "DeagleBullet";
    }
}
