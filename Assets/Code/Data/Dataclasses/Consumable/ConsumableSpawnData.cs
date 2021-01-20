using GridCell;
using UnityEngine;


/// <summary>Data class containing data for Cnsumable spawners
/// </summary>
[CreateAssetMenu(fileName = "NewSnakeData", menuName = "ScriptableObjects/Consumables/SpawnData", order = 1)]
public class ConsumableSpawnData : ScriptableObject
{
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private Consumable objectToSpawn;
    [SerializeField]
    private int maxObjectSpawnCount = 5;

    [SerializeField]
    private ConsumableData consumableData;
    public ConsumableData ConsumableData => consumableData;
    public Consumable ObjectToSpawn => objectToSpawn;
    public int MaxObjectCount => maxObjectSpawnCount;
    public float SpawnInterval => spawnInterval;

    public void CreateTimedSpawner(Cell[,] grid)
    {
        new GameObject("ConsumableSpawner").AddComponent<ConsumableSpawner>().InitializeSpawner(this, grid);
    }
}
