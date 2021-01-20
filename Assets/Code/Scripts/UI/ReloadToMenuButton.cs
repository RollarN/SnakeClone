using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>Pops up after game end, prompting player to reload level
/// </summary>
public class ReloadToMenuButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ReloadLevel);
        GameLoopUtility.OnGameEnd += ActivateObject;
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            ReloadLevel();
    }
    void ActivateObject()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    void OnDestroy() => GameLoopUtility.OnGameEnd -= ActivateObject;

    void ReloadLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

}
