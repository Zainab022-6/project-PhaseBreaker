using UnityEngine;

public class FloatingProp : MonoBehaviour
{
    public float speed = 1.0f;
    public float height = 0.5f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start() {
        posOffset = transform.position;
    }

    void Update() {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * height;
        transform.position = tempPos;
    }
}