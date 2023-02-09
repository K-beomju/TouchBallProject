using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BestScoreGroup : MonoBehaviour
{
    [SerializeField] private Text bestScoreText;

    private RectTransform rect;
    private CanvasGroup canvasGroup;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start() 
    {
        canvasGroup.DOFade(1,0.5f);
        rect.DOAnchorPosY(0, 0.5f);
    }

    public void ShowBestScore()
    {
        if (SecurityPlayerPrefs.HasKey("bestScore"))
        {
            bestScoreText.text = SecurityPlayerPrefs.GetInt("bestScore", default).ToString();
        }
    }

}
