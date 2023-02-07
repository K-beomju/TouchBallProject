using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextGroup : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;

    public void ShowCurrentScore(int score)
    {
        currentScoreText.text = score.ToString();
    }
}
