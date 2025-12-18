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
    private bool isSolid;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        SetGhost();
    }

    public void ApplyPhase(ColorType activeColor)
    {
        Debug.Log("ApplyPhase called on " + name);

        if (activeColor == blockColor)
        {
            SetSolid();
        }
    }

    private void SetSolid()
    {
        isSolid = true;

        boxCollider.isTrigger = false;   // ← WALKABLE
        bodyRenderer.material = solidMaterial;
        edgeRenderer.material = solidMaterial;

        Debug.Log(name + " is SOLID");
    }

    private void SetGhost()
    {
        isSolid = false;

        boxCollider.isTrigger = true;    // ← PASS THROUGH
        bodyRenderer.material = ghostMaterial;
        edgeRenderer.material = ghostMaterial;
    }
}