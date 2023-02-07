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

    public void GameOverDirect()
    {
        gameOverSq = DOTween.Sequence();
        gameOverSq.AppendInterval(1);
        gameOverSq.Append(rtm.DOAnchorPosY(100, 2));
        gameOverSq.Join(canvasGroup.DOFade(0, 1));
    }
}
