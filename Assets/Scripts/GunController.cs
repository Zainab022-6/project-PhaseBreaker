using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 8f;
    [SerializeField] private float rayOriginOffset = 0.3f;

    [Header("Gun Lights")]
    [SerializeField] private Light redLight;
    [SerializeField] private Light blueLight;

    private void Update()
    {
        // HOLD Right Click → RED beam
        if (Input.GetMouseButton(1))
        {
            Fire(ColorType.Red);
            EnableLight(redLight);
            DisableLight(blueLight);
        }
        // HOLD Left Click → BLUE beam
        else if (Input.GetMouseButton(0))
        {
            Fire(ColorType.Blue);
            EnableLight(blueLight);
            DisableLight(redLight);
        }
        else
        {
            // No input → lights off
            DisableLight(redLight);
            DisableLight(blueLight);
        }
    }

    private void Fire(ColorType color)
    {
        Vector3 origin = playerCamera.transform.position +
                         playerCamera.transform.forward * rayOriginOffset;

        Ray ray = new Ray(origin, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            PhaseBlock block = hit.collider.GetComponent<PhaseBlock>();
            if (block != null)
            {
                block.ApplyPhase(color);
            }
        }
    }

    private void EnableLight(Light light)
    {
        if (light != null && !light.enabled)
            light.enabled = true;
    }

    private void DisableLight(Light light)
    {
        if (light != null && light.enabled)
            light.enabled = false;
    }
}