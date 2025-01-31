using UnityEngine;

public class ShotgunShooting : MonoBehaviour
{
    public GameObject recoilEffectPrefab; // Optional: Visual effect for recoil
    public Transform firePoint; // The point where the recoil is triggered
    public float recoilForce = 15f;

    public Rigidbody2D playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            Debug.LogError("No Rigidbody2D found on the player!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Apply recoil force to the player
        playerRb.AddForce(-firePoint.right * recoilForce, ForceMode2D.Impulse);

        // Optional: Spawn visual recoil effect
        if (recoilEffectPrefab != null)
        {
            Instantiate(recoilEffectPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
