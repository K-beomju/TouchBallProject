using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CurrentScoreGroup : MonoBehaviour
{
    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text currentDescText;
    [SerializeField] private Text goldScoreText;

    private RectTransform getGoldTextRtm;
    private CanvasGroup canvasGroup;
    private RectTransform rtm;
    private Sequence gameOverSq;

    [SerializeField] private Transform ballObj;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rtm = GetComponent<RectTransform>();
        getGoldTextRtm = goldScoreText.GetComponent<RectTransform>();
    }

    public void ShowCurrentScore(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }

    [ContextMenu("ChangeGoldTextColor")]
    public void ChangeGoldTextColor()
    {
        getGoldTextRtm.gameObject.SetActive(true);
        goldScoreText.DOFade(1, 0);
        getGoldTextRtm.transform.position = ballObj.position;
        getGoldTextRtm.DOAnchorPosY(getGoldTextRtm.anchoredPosition.y + 100, 0.5f);
        goldScoreText.DOFade(0, 1f);

        currentScoreText.DOColor(Color.yellow, 0.3f).OnComplete(() => currentScoreText.DOColor(Color.white, 0.3f));
    }


    public void GameOverDirect()
    {
        gameOverSq = DOTween.Sequence();
        gameOverSq.AppendInterval(1);
        gameOverSq.Append(rtm.DOScale(new Vector3(1.2f, 1.2f, 0), 1));
        gameOverSq.Play();

        if (DataManager.Instance.CurrentScore > DataManager.Instance.BestScore)
        {
            currentDescText.DOText("Best Score!", 1, true, ScrambleMode.All).SetDelay(2);
        }

    }
}
