using UnityEngine;
using System.Collections;

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
    [Tooltip("0 = permanent (no revert). >0 = seconds until it reverts to ghost")]
    [SerializeField] private float revertTime = 0f;

    private BoxCollider boxCollider;
    private Coroutine revertCoroutine;

    private void Awake()
    {
        // Find collider safely (root -> children -> parent)
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null) boxCollider = GetComponentInChildren<BoxCollider>();
        if (boxCollider == null) boxCollider = GetComponentInParent<BoxCollider>();

        if (boxCollider == null)
            Debug.LogError($"PhaseBlock ERROR: No BoxCollider found on {name}");

        SetGhostImmediate();
    }

    // Called by GunController
    public void ApplyPhase(ColorType activeColor)
    {
        if (activeColor == blockColor)
            SetSolid();
        else
            SetGhost();
    }

    private void SetSolid()
    {
        // Stop any running revert
        if (revertCoroutine != null)
        {
            StopCoroutine(revertCoroutine);
            revertCoroutine = null;
        }

        if (boxCollider != null) boxCollider.isTrigger = false;
        if (bodyRenderer != null && solidMaterial != null) bodyRenderer.material = solidMaterial;
        if (edgeRenderer != null && solidMaterial != null) edgeRenderer.material = solidMaterial;

        // If revertTime > 0, start coroutine to go back to ghost after that time
        if (revertTime > 0f)
            revertCoroutine = StartCoroutine(RevertAfterSeconds(revertTime));
    }

    private void SetGhost()
    {
        // Cancel revert if going to ghost immediately
        if (revertCoroutine != null)
        {
            StopCoroutine(revertCoroutine);
            revertCoroutine = null;
        }

        SetGhostImmediate();
    }

    private void SetGhostImmediate()
    {
        if (boxCollider != null) boxCollider.isTrigger = true;
        if (bodyRenderer != null && ghostMaterial != null) bodyRenderer.material = ghostMaterial;
        if (edgeRenderer != null && ghostMaterial != null) edgeRenderer.material = ghostMaterial;
    }

    private IEnumerator RevertAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SetGhostImmediate();
        revertCoroutine = null;
    }

    // Helper to allow LevelManager or others to configure revert time at runtime
    public void SetRevertTime(float seconds)
    {
        revertTime = seconds;
    }

    // Expose block color for editor checking or managers
    public ColorType GetColorType() => blockColor;
}
