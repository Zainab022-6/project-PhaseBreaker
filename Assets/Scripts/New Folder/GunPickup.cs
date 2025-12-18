using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [Header("Assignments")]
    public Transform weaponHolder;
    public MonoBehaviour spinScript;

    [Header("Pickup Settings")]
    public Vector3 pickupPosition = Vector3.zero;
    public Vector3 pickupRotation = Vector3.zero;

    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || isPickedUp) return;

        PickUpGun();
    }

    private void PickUpGun()
    {
        isPickedUp = true;

        if (spinScript != null)
            spinScript.enabled = false;

        transform.SetParent(weaponHolder);
        transform.localPosition = pickupPosition;
        transform.localRotation = Quaternion.Euler(pickupRotation);

        GetComponent<Collider>().enabled = false;

        // IMPORTANT: enable gun logic ONLY NOW
        GunController gun = GetComponent<GunController>();
        if (gun != null)
            gun.enabled = true;

        // Prevent ray from hitting gun itself
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        Debug.Log("Gun PICKED UP → GunController ENABLED");
    }
}