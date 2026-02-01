using UnityEngine;

/// <summary>
/// Per-scene Respawn manager. Place one in each scene (Prologue, Level_Layout, Level3...).
/// Holds the current spawn position (default is the position assigned in inspector or player's start).
/// Does NOT persist across scenes.
/// </summary>
public class RespawnManager : MonoBehaviour
{
    [Header("Optional initial spawn")]
    [Tooltip("If left empty, PlayerRespawn will set spawn to the player's start position on Start.")]
    public Transform initialSpawnTransform;

    private Vector3 spawnPoint = Vector3.zero;

    private void Awake()
    {
        if (initialSpawnTransform != null)
            spawnPoint = initialSpawnTransform.position;
    }

    /// <summary>Set the current spawn point (e.g., checkpoint). Safe to call at runtime.</summary>
    public void SetSpawn(Vector3 point)
    {
        spawnPoint = point;
    }

    /// <summary>Get the current spawn point. If not set, returns Vector3.zero.</summary>
    public Vector3 GetSpawn()
    {
        return spawnPoint;
    }
}
