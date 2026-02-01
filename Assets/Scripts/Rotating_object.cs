using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = new Vector3(0f, 1f, 0f); // Y-axis by default
    public float rotationSpeed = 60f; // degrees per second

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
