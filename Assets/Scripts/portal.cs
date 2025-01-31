using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the object is already in the portalObjects set, do not teleport it again
        if (portalObjects.Contains(collision.gameObject))
        {
            return;
        }

        // Try to get the portal component from the destination
        if (destination.TryGetComponent(out portal destinationPortal))
        {
            // Add the object to the destination portal's portalObjects set to avoid immediate re-teleportation
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        // Move the colliding object to the destination's position
        collision.transform.position = destination.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the object from the portalObjects set when it exits the portal
        portalObjects.Remove(collision.gameObject);
    }
}
