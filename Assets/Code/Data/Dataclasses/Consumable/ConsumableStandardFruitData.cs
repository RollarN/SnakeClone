using GridCell;
using UnityEngine;


/// <summary>Data for the standard snake consumable - Increases tail size and score.
/// </summary>

[CreateAssetMenu(fileName = "newStandardConsumable", menuName = "ScriptableObjects/Consumable/StandardFruit", order = 5)]
public class ConsumableStandardFruitData : ConsumableData
{
    [SerializeField]
    private AudioClip audioClipWhenConsumed;
    [SerializeField]
    private int scoreIncrease;
    [SerializeField]
    private int tailNodesToAddWhenConsumed = 1;
    public AudioClip AudioClipWhenConsumed => audioClipWhenConsumed;
    /// <summary>Data for the standard snake consumable - Increases tail size and score.
    /// </summary>
    public override void OnConsumed(SnakeHead snakehead, Cell targetCell = null)
    {
        SnakeScoreHandler.UpdatedScore?.Invoke(scoreIncrease);
        snakehead.SnakeTail.TailNodesToAdd += tailNodesToAddWhenConsumed;

        OneShotAudioManager.PlayOneShot2D(AudioClipWhenConsumed);
        OneShotPFXManager.PlayPFX(targetCell.Vector2Position, PFXType.StdConsumableExplosion);
    }
}
