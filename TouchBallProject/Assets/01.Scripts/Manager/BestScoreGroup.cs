using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreGroup : MonoBehaviour
{
    [SerializeField] private Text bestScoreText;

    public void ShowBestScore()
    {
        if (SecurityPlayerPrefs.HasKey("bestScore"))
        {
            bestScoreText.text = SecurityPlayerPrefs.GetInt("bestScore", default).ToString();
        }
    }
}
