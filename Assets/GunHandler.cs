using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject deaglePrefab;
    public GameObject shotgunPrefab;

    private GameObject currentWeapon;
    private string currentWeaponName = "Deagle"; // Default weapon

    public Transform weaponSpawnPoint;

    void Start()
    {
        EquipWeapon(currentWeaponName);
    }

    void Update()
    {
        HandleWeaponSwitch();
    }

    private void EquipWeapon(string weaponName)
    {
        // Destroy the current weapon if one exists
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Load weapon from resources
        GameObject weaponPrefab = null;
        switch (weaponName)
        {
            case "Deagle":
                weaponPrefab = deaglePrefab ?? Resources.Load<GameObject>("Deagle");
                break;
            case "Shotgun":
                weaponPrefab = shotgunPrefab ?? Resources.Load<GameObject>("Shotgun");
                break;
            default:
                Debug.LogError("Invalid weapon name.");
                return;
        }

        // Instantiate the weapon and parent it to the spawn point
        if (weaponPrefab != null && weaponSpawnPoint != null)
        {
            currentWeapon = Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation, weaponSpawnPoint);
            currentWeaponName = weaponName; // Update the current weapon name
        }
        else
        {
            Debug.LogError("Weapon prefab or spawn point is missing.");
        }
    }

    private void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon("Deagle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon("Shotgun");
        }

        // Optional: Add scroll wheel support for weapon switching
        if (Input.mouseScrollDelta.y > 0)
        {
            EquipWeapon(currentWeaponName == "Deagle" ? "Shotgun" : "Deagle");
        }
    }
}
