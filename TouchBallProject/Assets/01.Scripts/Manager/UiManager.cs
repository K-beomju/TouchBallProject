using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoSingleton<UiManager>
{
    public CurrentScoreGroup currentScore;
    public BestScoreGroup bestScore;
    public HomeButton home;
    public BackGround back;

    protected override void Start()
    {
        bestScore.ShowBestScore();
    }

    public void GameStartUI()
    {
        currentScore.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(false);
    }

    public void GameOverUI()
    {
        home.gameObject.SetActive(true);
        home.ShowHomeButton();
        back.GameOver();
        //currentScore.GameOverDirect();
    }

}
