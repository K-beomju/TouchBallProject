using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    private Button button;
    private CanvasGroup canvasGroup;
    private RectTransform rect;

    private void Awake() 
    {
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();

        button.onClick.AddListener(() => LoadHome());
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowHomeButton()
    {
        Sequence sq = DOTween.Sequence();
        sq.AppendInterval(1);
        sq.Append(rect.DOAnchorPosY(-450, 1));
        sq.Join(canvasGroup.DOFade(1,1).SetDelay(1));
        sq.InsertCallback(3,() => canvasGroup.blocksRaycasts = true);
    }
}
