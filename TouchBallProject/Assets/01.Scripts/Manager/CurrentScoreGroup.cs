using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CurrentScoreGroup : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;

    private CanvasGroup canvasGroup;
    private RectTransform rtm;

    private Sequence gameOverSq;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rtm = GetComponent<RectTransform>();
    }

    public void ShowCurrentScore(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }

    public void ChangeGoldTextColor()
    {
        currentScoreText.DOColor(Color.yellow , 0.5f).OnComplete(() => currentScoreText.DOColor(Color.black, 0.5f));
    }


    public void GameOverDirect()
    {
        gameOverSq = DOTween.Sequence();
        gameOverSq.AppendInterval(1);
        gameOverSq.Append(rtm.DOScale(new Vector3(1.2f,1.2f,0), 1));
    }
}
