using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    private UiManager uiManager;


    private int _currentScore;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            uiManager.scoreTextGroup.ShowCurrentScore(value);
        }
    }


    private int _bestScore;
    public int BestScore
    {
        get => _bestScore;
        set => _bestScore = value;
    }


    private void Awake()
    {
        if(uiManager == null)
            uiManager = GetComponent<UiManager>();
    }


    public void CurrentAddScore()
    {
        CurrentScore++;
    }
}
