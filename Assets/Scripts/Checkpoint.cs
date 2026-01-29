using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    private void Reset()
    {
        // ensure trigger checkbox on collider is checked in editor by default
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // set spawn point at this object's position (or customize)
        Vector3 respawnPos = transform.position;
        RespawnManager.Instance.SetSpawn(respawnPos);
    }
}