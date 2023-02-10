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

    public GameObject itemGroup;
    public GameObject item;

    public void GameStartUI()
    {
        currentScore.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(false);
        backGroundMove.backStart = true;
    }

    public void GameOverUI()
    {
        home.gameObject.SetActive(true);
        home.ShowHomeButton();
        currentScore.GameOverDirect();
        itemGroup.SetActive(false);
        item.SetActive(false);
        ItemManager.Instance.starList.ForEach(x => x.GameOverStar());
        backGroundMove.backStart = false;

    }

}
