using GridCell;
using UnityEngine;
/// <summary>Component that plays PFX when activated, and then deactivates itself
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class OneShotPFXPlayer : MonoBehaviour
{
    public ParticleSystem PFXSystem => GetComponent<ParticleSystem>();
    /// <summary>Plays PFX at target location and Deactivates itself
    /// </summary>
    public void PlayPFX(Vector2 targetPosition)
    {
        gameObject.SetActive(true);
        transform.position = targetPosition;
        var playLength = PFXSystem.main.duration + PFXSystem.main.startLifetime.constant;
        PFXSystem.Play();
        Invoke(nameof(Disable), playLength);
    }
    public void PlayPfx(Cell targetCell) => PlayPFX(targetCell.Vector2Position);
    void Disable() => gameObject.SetActive(false);
}
