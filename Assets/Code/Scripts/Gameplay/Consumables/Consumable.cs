using UnityEngine;

/// <summary> Component that allows the object to be eaten by the Snake
/// </summary>
[RequireComponent(typeof(CellOccupant))]
[RequireComponent(typeof(SpriteRenderer))]
public class Consumable : MonoBehaviour, ISnakeTraversable
{
    private ConsumableData consumableData;
    private CellOccupant occupant;
    public ConsumableData ConsumableData
    {
        get => consumableData;
        set
        {
            consumableData = value;
            GetComponent<SpriteRenderer>().sprite = value.Sprite;
        }
    }
    public void Start()
    {
        occupant = GetComponent<CellOccupant>();
    }
    /// <summary> Triggers the OnConsumptionEffect and deactivates the consumable 
    /// </summary>
    public void OnTraversal(SnakeHead snakeHead)
    {

        if (!consumableData)
        {
            Debug.LogWarning("No consumableData found for: " + gameObject.name);
            gameObject.SetActive(false);
            return;
        }
        consumableData.OnConsumed(snakeHead, occupant.CurrentCell);
        gameObject.SetActive(false);
    }
}