using System;
using TMPro;
using UnityEngine;

/// <summary> UI-Component that handles the player score.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class SnakeScoreHandler : MonoBehaviour
{
    private float score;
    private TextMeshProUGUI textMeshPro;
    public static Action<int> UpdatedScore;

    public void Start()
    {

        textMeshPro = GetComponent<TextMeshProUGUI>();
        score = 0f;
        UpdatedScore += (int scoreIncrease) =>
        {
            score += scoreIncrease;
            textMeshPro.SetText("Score : " + score);
        };
    }


}
