using UnityEngine;

/// <summary>Data class containing references to all othner level classes
/// </summary>
[CreateAssetMenu(fileName = "newLevelData", menuName = "ScriptableObjects/LevelData/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public LevelGridData levelGridData;
    public SnakeData snakeData;
    [Header("Consumable")]
    public ConsumableSpawnData[] TimedSpawns;
    [Header("Music")]
    public AudioClip BackgroundMusicSong;

    /// <summary>Initializes the game. Should only be called once
    /// </summary>
    public void InitializeGame()
    {
        if (!FindObjectOfType<SnakeScoreHandler>())
            Debug.LogWarning("No scorehandler was found in scene before play. No score will be shown");

        #region MusicPlayer
        GameObject backgroundMusicPlayer = GameObject.Find("MusicPlayer");
        if (!backgroundMusicPlayer && BackgroundMusicSong)
        {
            backgroundMusicPlayer = new GameObject("MusicPlayer");
        }
        if (BackgroundMusicSong)
        {
            backgroundMusicPlayer.AddComponent<AudioSource>().clip = BackgroundMusicSong;
            backgroundMusicPlayer.GetComponent<AudioSource>().loop = true;
            backgroundMusicPlayer.GetComponent<AudioSource>().Play();
            backgroundMusicPlayer.GetComponent<AudioSource>().volume = 0.1f;
            DontDestroyOnLoad(backgroundMusicPlayer);
        }
        #endregion MusicPlayer

        //Initializes the static classes
        OneShotAudioManager.SetupObjectPool(5);
        OneShotPFXManager.SetUpObjectPool();

        //Create Grid
        var cellGrid = levelGridData.GenerateGrid();
        CellGridUtility.Grid = cellGrid;

        //Create Player
        var Snake = snakeData.GenerateSnake();
        Snake.GetComponent<CellOccupant>().CurrentCell = cellGrid
            [levelGridData.SnakeStartPositionX, levelGridData.SnakeStartPositionY];

        //Create Spawners
        for (int i = 0; i < TimedSpawns.Length; i++)
        {
            TimedSpawns[i].CreateTimedSpawner(cellGrid);
        }

        //Place Camera and set settings
        var GridSizeX = levelGridData.GridSizeX;
        var GridSizeY = levelGridData.GridSizeY;
        Camera.main.transform.position = new Vector3((GridSizeX - 1) / 2, (GridSizeY * 1.1f - 1) / 2, -10);
        Camera.main.orthographicSize = GridSizeY / 1.75f;

        GameLoopUtility.OnGameStart?.Invoke();
    }
}
