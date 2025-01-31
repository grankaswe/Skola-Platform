using UnityEngine;

public class PlayerGunAttachment : MonoBehaviour
{
    public GameObject deaglePrefab; // Prefab for the Deagle
    private Transform handPoint;    // The attachment point for the weapon
    public Transform firePoint;     // FirePoint for bullets
    private GameObject attachedWeapon; // The currently attached weapon

    private Camera mainCamera;      // Reference to the main camera

    private void Start()
    {
        mainCamera = Camera.main;

        // Ensure the "HandPoint" exists
        handPoint = transform.Find("HandPoint");
        if (handPoint == null)
        {
            Debug.Log("No HandPoint found. Creating one automatically.");
            GameObject newHandPoint = new GameObject("HandPoint");
            newHandPoint.transform.SetParent(transform, false);
            newHandPoint.transform.localPosition = new Vector2(1f, 0f); // Adjust position if needed
            handPoint = newHandPoint.transform;
        }

        // Set default fire point position
        firePoint.position = handPoint.position;

        // Default weapon is the Deagle
        EquipWeapon();
    }

    private void Update()
    {
        AimWeapon(); // Rotate weapon to face the mouse
    }

    public void EquipWeapon()
    {
        // Deactivate the current weapon if there is one
        if (attachedWeapon != null)
        {
            Destroy(attachedWeapon);
        }

        // Instantiate and attach the Deagle
        GameObject weaponPrefab = deaglePrefab ?? Resources.Load<GameObject>("Deagle");
        if (weaponPrefab != null)
        {
            attachedWeapon = Instantiate(weaponPrefab, handPoint.position, handPoint.rotation);
            attachedWeapon.transform.SetParent(handPoint, false);
            attachedWeapon.transform.localPosition = Vector2.zero;
            attachedWeapon.transform.localRotation = Quaternion.identity;
            Debug.Log("Deagle equipped.");
        }
        else
        {
            Debug.LogError("Deagle prefab not found! Check Resources or Inspector assignment.");
        }
    }

    private void AimWeapon()
    {
        if (attachedWeapon == null) return;

        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure we're working in 2D

        // Calculate the direction vector
        Vector2 aimDirection = (mousePosition - firePoint.position).normalized;

        // Rotate the weapon to face the mouse
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        handPoint.rotation = Quaternion.Euler(0, 0, angle);

        // Flip the weapon vertically if aiming to the left
        if (angle > 90 || angle < -90)
        {
            attachedWeapon.transform.localScale = new Vector3(1, -1, 1); // Flip vertically
        }
        else
        {
            attachedWeapon.transform.localScale = new Vector3(1, 1, 1); // Default scale
        }
    }
}
