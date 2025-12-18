using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public float amplitude = 10f; // How high it floats
    public float frequency = 2f;  // How fast it floats
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start() {
        posOffset = transform.localPosition;
    }

    void Update() {
        tempPos = posOffset;
        // Uses a Sine wave to create a smooth up/down loop
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.localPosition = tempPos;
    }
}
