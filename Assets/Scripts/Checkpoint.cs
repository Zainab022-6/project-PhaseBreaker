using UnityEngine;

/// <summary>
/// Attach to a small trigger GameObject placed at checkpoint locations in Level 3.
/// When player touches it, it updates the local RespawnManager's spawn point.
/// For Level 1 & 2 we won't add checkpoint objects so this does nothing there.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [Header("Optional: custom respawn offset (y)")]
    [Tooltip("If you want the player to respawn slightly above the checkpoint, add a Y offset.")]
    public float respawnYOffset = 0.6f;

    private void Reset()
    {
        // ensure trigger checkbox on collider is checked in editor by default
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        RespawnManager mgr = FindObjectOfType<RespawnManager>();
        if (mgr == null)
        {
            Debug.LogWarning("Checkpoint: No RespawnManager found in the scene.");
            return;
        }

        Vector3 respawnPos = transform.position + Vector3.up * respawnYOffset;
        mgr.SetSpawn(respawnPos);
    }
}