using UnityEngine;
using UnityEngine.UI;
/// <summary>Holds the level data and initializes the game from the GameInitializer
/// </summary>
public class GameStartButton : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;
    [SerializeField]
    private KeyCode keycode;
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
        Time.timeScale = 0f;
        GameLoopUtility.OnGameStart += DisableSelf;
    }

    void Update()
    {
        if (Input.GetKeyUp(keycode))
            StartGame();
    }
    public void StartGame()
    {
        if (!gameObject.activeSelf)
            return;
        levelData.InitializeGame();
        Time.timeScale = 1f;
    }
    public void DisableSelf()
    {
        gameObject.SetActive(false);
        GameLoopUtility.OnGameStart -= DisableSelf;

    }
}
