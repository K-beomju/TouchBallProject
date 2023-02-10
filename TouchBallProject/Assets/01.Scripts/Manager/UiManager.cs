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
    public BackGround back;

    public GameObject itemGroup;
    public GameObject item;

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
        currentScore.GameOverDirect();
        itemGroup.SetActive(false);
        item.SetActive(false);
        ItemManager.Instance.starList.ForEach(x => x.GameOverStar());

    }

}
