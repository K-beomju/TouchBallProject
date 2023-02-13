using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EndButtonGroup : MonoBehaviour
{
    private RectTransform rtm;
    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        rtm = GetComponent<RectTransform>();
    }

    public void EndShowButton()
    {
        Sequence sq = DOTween.Sequence();
        sq.AppendInterval(1);
        sq.Append(rtm.DOAnchorPosY(-100, 1));
        sq.Join(canvas.DOFade(1, 1).SetDelay(1));
        sq.InsertCallback(3, () => canvas.blocksRaycasts = true);
    }
}
