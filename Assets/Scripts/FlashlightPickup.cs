using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public GameObject flashlight; // Reference to the flashlight object
    public FlashlightBehaviour playerFlashlightScript; // Reference to the PlayerFlashlight script
    public UIManager uiManager;

    private bool canPickUp = false;

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            // Ensure the PlayerFlashlight script reference is assigned
            if (playerFlashlightScript != null)
            {
                playerFlashlightScript.PickupFlashlight();
                flashlight.SetActive(false); // Disable the flashlight object in the scene
            }
            else
            {
                Debug.LogError("PlayerFlashlight script reference is not assigned.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = true;
            uiManager.UpdateInfoText("Press E to pick up the flashlight.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
            Debug.Log("Flashlight out of range.");
        }
    }
}
