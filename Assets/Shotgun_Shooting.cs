using UnityEngine;

[RequireComponent(typeof(PlayerGunAttachment))]
public class ShotgunShootingScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody; // Player's Rigidbody2D
    [SerializeField] private float recoilForce = 10f;     // Force of recoil
    [SerializeField] private float maxGravity = 1f;       // Gravity scale for normal movement

    private bool canShoot = false; // Determines if the shotgun can shoot

    private void Start()
    {
        PlayerGunAttachment.OnWeaponChanged += HandleWeaponChanged;

        // Ensure gravity scale is correct on start
        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = maxGravity;
        }
    }

    private void OnDestroy()
    {
        PlayerGunAttachment.OnWeaponChanged -= HandleWeaponChanged;
    }

    private void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0)) // Shoot on left mouse click
        {
            ApplyRecoil();
        }
    }

    private void HandleWeaponChanged(string weaponName)
    {
        canShoot = weaponName == "Shotgun";
    }

    private void ApplyRecoil()
    {
        if (playerRigidbody == null)
        {
            Debug.LogError("Player Rigidbody not assigned!");
            return;
        }

        // Calculate direction from player to mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = playerRigidbody.position;

        // Calculate recoil direction
        Vector2 recoilDirection = (playerPosition - mousePosition).normalized;

        // Restrict recoil to upward and horizontal
        if (recoilDirection.y < 0)
        {
            recoilDirection.y = 0; // Prevent downward movement
        }

        // Normalize recoil direction and apply force
        recoilDirection = recoilDirection.normalized;
        playerRigidbody.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);

        Debug.Log($"Shotgun recoil applied! Direction: {recoilDirection}, Force: {recoilForce}");

        // Restore gravity immediately (if modified due to recoil)
        playerRigidbody.gravityScale = maxGravity;
    }
}
