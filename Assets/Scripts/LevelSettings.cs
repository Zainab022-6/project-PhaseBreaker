using UnityEngine;

/// <summary>
/// Scene-level settings: sets PhaseBlock revert time for all blocks on Start.
/// Place one LevelSettings object in each scene and set blockRevertTime accordingly.
/// Level 1: set 0 (permanent). Level 2: set 10. Level 3: set 6 (or whatever).
/// </summary>
public class LevelSettings : MonoBehaviour
{
    [Tooltip("0 = permanent (no revert). >0 = seconds until block reverts to ghost.")]
    public float blockRevertTime = 0f;

    private void Start()
    {
        // find all PhaseBlock components and set revert time
        var blocks = FindObjectsOfType<PhaseBlock>();
        foreach (var b in blocks)
        {
            b.SetRevertTime(blockRevertTime);
        }
    }
}