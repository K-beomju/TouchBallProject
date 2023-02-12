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
        set 
        {
            _bestScore = value;
            UiManager.Instance.bestScore.ShowBestScore(value);
        } 
        
    }

    private int _star;
    public int Star
    {
        get => _star;
        set 
        {
            _star = value;
            UiManager.Instance.starGroup.ShowStar(value);
        } 
    }

    private void Awake() 
    {
        if (SecurityPlayerPrefs.HasKey("bestScore"))
        BestScore = SecurityPlayerPrefs.GetInt("bestScore", default);
        if(SecurityPlayerPrefs.HasKey("star"))
        Star = SecurityPlayerPrefs.GetInt("star", default);

    }

    public void CurrentAddScore(int value = 1)
    {
        CurrentScore += value;
    }

    public void AddStar(int value = 1)
    {
        Star += value;
    }

    public void MinusStar(int cost)
    {
        Star -= cost;
        SecurityPlayerPrefs.SetInt("star", Star);

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

        SecurityPlayerPrefs.SetInt("star", Star);
    }
}
