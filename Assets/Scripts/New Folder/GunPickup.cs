using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [Header("Assignments")]
    public Transform weaponHolder; // The invisible spot on your camera
    public MonoBehaviour spinScript; // The script that makes it float/spin (drag it here)

    [Header("Gun Settings")]
    public Vector3 pickupPosition = Vector3.zero; 
    public Vector3 pickupRotation = Vector3.zero;

    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object walking into us is the Player
        // (Make sure your First Person Controller has the tag "Player")
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PickUpGun();
        }
    }

    void PickUpGun()
    {
        isPickedUp = true;

        // 1. Disable the spinning/floating script so it stops moving
        if (spinScript != null)
        {
            spinScript.enabled = false;
        }

        // 2. Attach the gun to the WeaponHolder (The Camera's child)
        transform.SetParent(weaponHolder);

        // 3. Reset position and rotation to snap perfectly to the hand
        transform.localPosition = pickupPosition;
        transform.localRotation = Quaternion.Euler(pickupRotation);

        // 4. Disable the Collider so it doesn't block the player's movement
        GetComponent<Collider>().enabled = false;
        
        // 5. FUTURE: Enable your Shooting/Light Script here!
        // GetComponent<PhaseGunController>().enabled = true;
    }
}