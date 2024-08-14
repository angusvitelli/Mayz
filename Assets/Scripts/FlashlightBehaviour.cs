using UnityEngine;
public class FlashlightBehaviour : MonoBehaviour
{
    public GameObject flashlight; // Reference to the flashlight object
    public Light flashlightLight; // Reference to the flashlight light
    public Transform flashlightHolder; // The point on the player where the flashlight should attach
    public UIManager uiManager;

    void Start()
    {
        flashlightLight.enabled = false; // Start with the flashlight off
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightLight.enabled = !flashlightLight.enabled; // Toggle the flashlight on and off
        }
    }

    public void PickupFlashlight()
    {
        // Attach the flashlight light to the player
        flashlight.transform.SetParent(flashlightHolder);
        flashlight.transform.localPosition = Vector3.zero; // Adjust position if needed
        flashlight.transform.localRotation = Quaternion.identity; // Adjust rotation if needed
        
        if (flashlightLight.enabled!=true)
        {
            uiManager.UpdateInfoTextForToggle("Press F to toggle the flashlight.");
            flashlightLight.enabled = true;
        }
    }
}
