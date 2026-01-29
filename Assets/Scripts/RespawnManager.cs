using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance { get; private set; }

    private Vector3 spawnPoint = Vector3.zero;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetSpawn(Vector3 point)
    {
        spawnPoint = point;
    }

    public Vector3 GetSpawn()
    {
        return spawnPoint;
    }
}