using UnityEngine;

public class PhaseBlock : MonoBehaviour
{
    [Header("Renderers")]
    [SerializeField] private MeshRenderer bodyRenderer;
    [SerializeField] private MeshRenderer edgeRenderer;

    [Header("Materials")]
    [SerializeField] private Material ghostMaterial;
    [SerializeField] private Material solidMaterial;

    [Header("Settings")]
    [SerializeField] private ColorType blockColor;

    private BoxCollider boxCollider;

    private void Awake()
    {
        // Find collider safely (parent / child proof)
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null) boxCollider = GetComponentInChildren<BoxCollider>();
        if (boxCollider == null) boxCollider = GetComponentInParent<BoxCollider>();

        if (boxCollider == null)
            Debug.LogError($"PhaseBlock ERROR: No BoxCollider found on {name}");

        SetGhost();
    }

    public void ApplyPhase(ColorType activeColor)
    {
        Debug.Log($"ApplyPhase → {name} | Active: {activeColor} | Block: {blockColor}");

        if (activeColor == blockColor)
            SetSolid();
        else
            SetGhost();
    }

    private void SetSolid()
    {
        boxCollider.isTrigger = false;
        bodyRenderer.material = solidMaterial;
        edgeRenderer.material = solidMaterial;

        Debug.Log($"{name} → SOLID");
    }

    private void SetGhost()
    {
        boxCollider.isTrigger = true;
        bodyRenderer.material = ghostMaterial;
        edgeRenderer.material = ghostMaterial;

        Debug.Log($"{name} → GHOST");
    }
}