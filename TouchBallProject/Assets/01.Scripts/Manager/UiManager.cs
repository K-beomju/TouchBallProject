using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoSingleton<UiManager>
{
    public CurrentScoreGroup currentScore;
    public BestScoreGroup bestScore;

    protected override void Start()
    {
        bestScore.ShowBestScore();
    }

    public void GameStartUI()
    {
        currentScore.gameObject.SetActive(true);
    }

    public void GameOverUI()
    {
        currentScore.GameOverDirect();
    }

}
