using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class PlayerRespawn : MonoBehaviour
{
    [Tooltip("Y below this value means player fell; auto-respawn")]
    public float fallY = -12f;

    [Tooltip("Optional delay before respawn")]
    public float respawnDelay = 0.6f;

    private bool isRespawning = false;

    private void Start()
    {
        // On start, if no spawn set, set current position as spawn
        if (RespawnManager.Instance != null && RespawnManager.Instance.GetSpawn() == Vector3.zero)
        {
            RespawnManager.Instance.SetSpawn(transform.position);
        }
    }

    private void Update()
    {
        if (isRespawning) return;

        if (transform.position.y < fallY)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);

        Vector3 spawnPos = RespawnManager.Instance != null ? RespawnManager.Instance.GetSpawn() : Vector3.zero;

        // Try to move player safely depending on controller type
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = spawnPos;
        }
        else
        {
            // If CharacterController is used
            var charCtrl = GetComponent<CharacterController>();
            if (charCtrl != null)
            {
                charCtrl.enabled = false;
                transform.position = spawnPos;
                charCtrl.enabled = true;
            }
            else
            {
                transform.position = spawnPos;
            }
        }

        isRespawning = false;
    }
}