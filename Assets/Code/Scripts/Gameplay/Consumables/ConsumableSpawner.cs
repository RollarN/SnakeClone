using GridCell;
using UnityEngine;

/// <summary>Component that spawns Consumables at random positions at a set interval.
/// Should only be Instantiated from Levelgenerator at start.
/// </summary>
public class ConsumableSpawner : MonoBehaviour, ITimerTickOverrider
{
    private ObjectPool ObjectPool;
    [SerializeField]
    [Range(0, 50)]
    private float spawnInterval;
    public float TickIntervalTime => spawnInterval;

    /// <summary>Initializes the spawner, Objectpooling the consumables and sets a timer for them to spawn
    /// Should only be called when initializing the game at start.
    /// </summary>
    public void InitializeSpawner(ConsumableSpawnData consumableSpawnData, Cell[,] grid)
    {
        var targetObject = consumableSpawnData.ObjectToSpawn.gameObject;
        var objCount = consumableSpawnData.MaxObjectCount;
        ObjectPool = new ObjectPool(objCount, targetObject, transform);
        spawnInterval = consumableSpawnData.SpawnInterval;

        var timerComponent = gameObject.AddTimerComponentOverriden(this);
        timerComponent.OnTimerTick += () => SpawnConsumable(consumableSpawnData);
    }

    /// <summary>Spawns a consumable from the objectpool at a random position in the grid
    private void SpawnConsumable(ConsumableSpawnData spawnData)
    {
        (bool foundCell, Cell targetEmptyCell) = CellGridUtility.FindRandomEmptyCell(40);
        if (!foundCell)
            return;

        var objectToSpawn = ObjectPool.Rent(true);

        //Assigns data. Can't be done when initializing for some reason.
        objectToSpawn.GetComponent<Consumable>().ConsumableData = spawnData.ConsumableData;
        objectToSpawn.GetComponent<CellOccupant>().CurrentCell = targetEmptyCell;

        var spawnAudioClip = spawnData.ConsumableData.AudioClipWhenSpawned;
        if (spawnAudioClip)
            OneShotAudioManager.PlayOneShot2D(spawnAudioClip);
    }
}
