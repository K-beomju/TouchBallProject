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
            UiManager.Instance.scoreTextGroup.ShowCurrentScore(value);
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
}
