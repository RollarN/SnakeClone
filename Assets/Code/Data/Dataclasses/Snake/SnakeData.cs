using UnityEngine;


/// <summary>Data class containing Data and logic for creating the snake
/// </summary>
[CreateAssetMenu(fileName = "NewSnakeData", menuName = "ScriptableObjects/SnakeData", order = 1)]
public class SnakeData : ScriptableObject
{
    public float SnakeMovementsPerSecond;
    public CellOccupant bodyPartPrefab;
    public SnakeHead SnakeHeadPrefab;

    public SnakeHead GenerateSnake()
    {
        //Create Player
        if (!SnakeHeadPrefab)
            throw new System.NullReferenceException("SnakeHead prefab must be set");
        if (!bodyPartPrefab)
            throw new System.NullReferenceException("SnakeBodypart prefab must be set");
        var SnakeParent = new GameObject("SnakeParent").transform;

        var Snake = Instantiate(SnakeHeadPrefab, SnakeParent);
        Snake.GetComponent<TimedCellMovement>().MovementsPerSecond = SnakeMovementsPerSecond;

        Snake.GetComponent<SnakeTailManager>().InitializeTailPool(bodyPartPrefab);
        return Snake;
    }
}
