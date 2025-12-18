using UnityEngine;
using System.Linq;

public class GunController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 6f;
    [SerializeField] private float rayOriginOffset = 0.5f;

    [Header("Gun Lights")]
    [SerializeField] private Light redLight;
    [SerializeField] private Light blueLight;

    // Layers to ignore (Player/Gun/Ignore Raycast)
    private int rayMask;

    private void Awake()
    {
        rayMask = ~LayerMask.GetMask("Player", "Gun", "Ignore Raycast");

        // Ensure lights start off
        if (redLight != null) redLight.enabled = false;
        if (blueLight != null) blueLight.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Fire(ColorType.Red);
            if (redLight != null) redLight.enabled = true;
            if (blueLight != null) blueLight.enabled = false;
        }
        else if (Input.GetMouseButton(0))
        {
            Fire(ColorType.Blue);
            if (blueLight != null) blueLight.enabled = true;
            if (redLight != null) redLight.enabled = false;
        }
        else
        {
            if (redLight != null) redLight.enabled = false;
            if (blueLight != null) blueLight.enabled = false;
        }
    }

    private void Fire(ColorType color)
    {
        if (playerCamera == null) return;

        Vector3 origin = playerCamera.transform.position + playerCamera.transform.forward * rayOriginOffset;
        Vector3 dir = playerCamera.transform.forward;

        // Get all hits including triggers
        RaycastHit[] hits = Physics.RaycastAll(origin, dir, range, rayMask, QueryTriggerInteraction.Collide);
        if (hits == null || hits.Length == 0) return;

        // nearest first
        hits = hits.OrderBy(h => h.distance).ToArray();

        // find first PhaseBlock
        foreach (var h in hits)
        {
            PhaseBlock block =
                h.collider.GetComponent<PhaseBlock>() ??
                h.collider.GetComponentInParent<PhaseBlock>() ??
                h.collider.GetComponentInChildren<PhaseBlock>();

            if (block != null)
            {
                block.ApplyPhase(color);
                return;
            }
        }
    }
}
