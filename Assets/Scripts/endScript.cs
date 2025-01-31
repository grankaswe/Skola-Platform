using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player"; // Tag assigned to the player object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the portal has the correct tag
        if (collision.CompareTag(playerTag))
        {
            Debug.Log("Player entered the portal. Returning to the menu...");
            SceneManager.LoadScene(0); // Load the menu scene at index 0
        }
    }
}
