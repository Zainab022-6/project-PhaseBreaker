using UnityEngine;
using System.Collections;

/// <summary>
/// Attach to the Player root object. Detects "falling" and will reposition the player to the current respawn point.
/// Works with Rigidbody or CharacterController and temporarily disables movement/look scripts while respawning.
/// </summary>
[RequireComponent(typeof(Transform))]
public class PlayerRespawn : MonoBehaviour
{
    [Header("Fall detection")]
    [Tooltip("If true, we compute fallY = spawnY - relativeFallDistance. Safer across levels.")]
    public bool useRelativeFall = true;
    [Tooltip("If useRelativeFall is true, player is considered fallen when y < spawnY - relativeFallDistance.")]
    public float relativeFallDistance = 10f;
    [Tooltip("If useRelativeFall is false, fallY is used as an absolute Y threshold.")]
    public float fallY = -12f;

    [Header("Respawn timing")]
    [Tooltip("Seconds to wait before teleporting back")]
    public float respawnDelay = 0.6f;

    [Header("Optional control script names to disable during respawn")]
    public string movementScriptName = "FirstPersonMovement";
    public string lookScriptName = "FirstPersonLook";

    private RespawnManager respawnManager;
    private bool isRespawning = false;

    private void Start()
    {
        respawnManager = FindObjectOfType<RespawnManager>();

        // If no RespawnManager assigned or it has no spawn point, initialize spawn to player's current position
        if (respawnManager != null && respawnManager.GetSpawn() == Vector3.zero)
        {
            respawnManager.SetSpawn(transform.position);
        }
    }

    private void Update()
    {
        if (isRespawning) return;

        Vector3 spawn = respawnManager != null ? respawnManager.GetSpawn() : Vector3.zero;
        float threshold;

        if (useRelativeFall)
        {
            threshold = spawn.y - relativeFallDistance;
        }
        else
        {
            threshold = fallY;
        }

        if (transform.position.y < threshold)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        // Optionally disable player controls
        MonoBehaviour moveScript = (MonoBehaviour)GetComponent(movementScriptName);
        MonoBehaviour lookScript = (MonoBehaviour)GetComponent(lookScriptName);

        if (moveScript != null) moveScript.enabled = false;
        if (lookScript != null) lookScript.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // Reposition player
        Vector3 spawn = respawnManager != null ? respawnManager.GetSpawn() : Vector3.zero;

        // Try Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = spawn;
        }
        else
        {
            // Try CharacterController
            var charCtrl = GetComponent<CharacterController>();
            if (charCtrl != null)
            {
                charCtrl.enabled = false;
                transform.position = spawn;
                charCtrl.enabled = true;
            }
            else
            {
                transform.position = spawn;
            }
        }

        // small safety pause to let physics settle
        yield return null;

        if (moveScript != null) moveScript.enabled = true;
        if (lookScript != null) lookScript.enabled = true;

        isRespawning = false;
    }

    // Helper to find a component by name on this GameObject (returns null if not found or not a MonoBehaviour)
    private Component GetComponent(string typeName)
    {
        if (string.IsNullOrEmpty(typeName)) return null;
        var comps = GetComponents<MonoBehaviour>();
        foreach (var c in comps)
        {
            if (c.GetType().Name == typeName) return c;
        }
        return null;
    }
}
