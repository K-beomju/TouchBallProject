using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoSingleton<UiManager>
{
    public CurrentScoreGroup currentScore;
    public BestScoreGroup bestScore;
    public StarGroupPanel starGroup;
    public HomeButton home;
    public BackGroundMove backGroundMove;
    public EndButtonGroup endButtonGroup;

    public GameObject itemGroup;
    public GameObject item;
    public GameObject InterstitialRetryPopup;

    [SerializeField] private CanvasGroup titleGroup;

    public void GameStartUI()
    {
        currentScore.gameObject.SetActive(true);
        currentScore.FadeIn();
        backGroundMove.backStart = true;
        titleGroup.DOFade(0,0.3f).OnComplete(() => titleGroup.gameObject.SetActive(false));
    }

    public void GameOverUI()
    {
        endButtonGroup.gameObject.SetActive(true);
        endButtonGroup.EndShowButton();
        currentScore.GameOverDirect();
        itemGroup.SetActive(false);
        item.SetActive(false);
        backGroundMove.backStart = false;

    }


}
