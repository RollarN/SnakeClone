using GridCell;
using UnityEngine;

/// <summary>Abstract class containing ConsumableData. Inheritance is used to allow for abstract fields.
/// </summary>
public abstract class ConsumableData : ScriptableObject
{
    [SerializeField]
    private AudioClip audioClipWhenSpawned;
    [SerializeField]
    private Sprite sprite;
    public virtual Sprite Sprite => sprite;
    public virtual AudioClip AudioClipWhenSpawned => audioClipWhenSpawned;
    public virtual void OnConsumed(SnakeHead snakehead, Cell targetCell)
    {
    }
}
