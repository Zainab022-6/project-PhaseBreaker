using UnityEngine;
using System.Linq;

public class GunController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 8f;
    [SerializeField] private float rayOriginOffset = 0.5f;

    [Header("Gun Lights")]
    [SerializeField] private Light redLight;
    [SerializeField] private Light blueLight;

    // Layers to ignore (player/gun/ignore)
    private int rayMask;

    private void Awake()
    {
        // Build a mask that ignores these layers
        rayMask = ~LayerMask.GetMask("Player", "Gun", "Ignore Raycast");

        // Ensure lights start disabled
        DisableLight(redLight);
        DisableLight(blueLight);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Fire(ColorType.Red);
            EnableLight(redLight);
            DisableLight(blueLight);
        }
        else if (Input.GetMouseButton(0))
        {
            Fire(ColorType.Blue);
            EnableLight(blueLight);
            DisableLight(redLight);
        }
        else
        {
            DisableLight(redLight);
            DisableLight(blueLight);
        }
    }

    private void Fire(ColorType color)
    {
        if (playerCamera == null)
        {
            Debug.LogError("GunController: playerCamera is not assigned!");
            return;
        }

        Vector3 origin = playerCamera.transform.position + playerCamera.transform.forward * rayOriginOffset;
        Vector3 dir = playerCamera.transform.forward;

        // Visual debug: visible in Scene while playing
        Debug.DrawRay(origin, dir * range, (color == ColorType.Red) ? Color.red : Color.blue, 0.1f);

        Debug.Log($"GunController: Ray origin={origin} dir={dir} range={range}");

        // Use RaycastAll AND include triggers (Collide)
        RaycastHit[] hits = Physics.RaycastAll(origin, dir, range, rayMask, QueryTriggerInteraction.Collide);

        if (hits == null || hits.Length == 0)
        {
            Debug.Log("GunController: RaycastAll found no hits");
            return;
        }

        // Sort hits by distance so we check nearest first
        hits = hits.OrderBy(h => h.distance).ToArray();

        // Print each hit for debugging
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log($"  Hit[{i}] -> {hits[i].collider.name} (distance {hits[i].distance:F2})");
        }

        // Find the first PhaseBlock among the hits
        PhaseBlock foundBlock = null;
        RaycastHit foundHit = new RaycastHit();
        foreach (var h in hits)
        {
            var block = h.collider.GetComponent<PhaseBlock>() ??
                        h.collider.GetComponentInParent<PhaseBlock>() ??
                        h.collider.GetComponentInChildren<PhaseBlock>();

            if (block != null)
            {
                foundBlock = block;
                foundHit = h;
                break;
            }
        }

        if (foundBlock != null)
        {
            Debug.Log($"GunController: Found PhaseBlock {foundBlock.name} at distance {foundHit.distance:F2}");
            foundBlock.ApplyPhase(color);
        }
        else
        {
            Debug.Log("GunController: Hit objects, but none had PhaseBlock");
        }
    }

    private void EnableLight(Light light)
    {
        if (light && !light.enabled) light.enabled = true;
    }

    private void DisableLight(Light light)
    {
        if (light && light.enabled) light.enabled = false;
    }
}
