using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    private int _currentScore;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            UiManager.Instance.currentScore.ShowCurrentScore(value);
        }
    }


    private int _bestScore;
    public int BestScore
    {
        get => _bestScore;
        set => _bestScore = value;
    }

    public void CurrentAddScore()
    {
        CurrentScore++;
    }

    public void UpdateBestScore()
    {
        if(SecurityPlayerPrefs.HasKey("bestScore"))
        {
            if(CurrentScore > SecurityPlayerPrefs.GetInt("bestScore", default))
            {
                BestScore = CurrentScore;
                SecurityPlayerPrefs.SetInt("bestScore", BestScore);
            }
        }
        else
        {
            BestScore = CurrentScore;
            SecurityPlayerPrefs.SetInt("bestScore", BestScore);
        }
    }
}
