using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InterstitialRetryPopup : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup canvasGroup;
    private bool isGameOver = false;
    private bool isRetry = false;

    public Ball ball;
    public Press press;
    public Button retryButton;


    private void OnEnable()
    {
        canvasGroup.DOFade(1, 1);
    }

    private void Update()
    {
        if (slider.value <= 0 && !isGameOver && !isRetry)
        {
            EndGame();
            isGameOver = true;
        }
        else
        {
            slider.value -= speed * Time.deltaTime;
        }
    }

    // 전면 광고보게 하고 실행하게 
    public void InterstitialRetry()
    {
        retryButton.interactable = false;
        isRetry = true;
        AdManager.Instance.ShowRewardedInterstitialAd((onRewardEarend) =>
        {
            StartCoroutine(AdManager.Instance.ExecuteAfterFrame(() =>
            {
                if (onRewardEarend)
                {
                    RetryGame();
                }
                else
                {

                }
            }));
        });
    }

    public void RetryGame()
    {
        ball.isRotate = true;
        ball.isRetry = true;
        ball.moveSpeed = 1;
        ball.gameObject.SetActive(true);
        ball.transform.position = Vector3.zero;
        ball.rb.velocity = Vector2.zero;
        ball.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        gameObject.SetActive(false);
        press.transform.position = new Vector3(press.transform.position.x, 0, 0);
        isRetry = false;
    }

    public void EndGame()
    {
        canvasGroup.DOFade(0, 1).OnComplete(() => gameObject.SetActive(false));
        canvasGroup.interactable = false;
        press.GameOverDirect();
        UiManager.Instance.GameOverUI();
        DataManager.Instance.UpdateBestScore();
    }
}
