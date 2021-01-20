using TMPro;
using UnityEngine;
/// <summary> UI-Component that shows the current time
/// </summary>
public class GameTimeDisplay : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int minutes = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
        int seconds = Mathf.FloorToInt(Time.timeSinceLevelLoad % 60);
        textMeshPro.SetText("Time: " + minutes + " : " + seconds.ToString("00."));
    }
}
