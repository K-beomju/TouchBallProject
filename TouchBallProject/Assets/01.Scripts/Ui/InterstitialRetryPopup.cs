using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InterstitialRetryPopup : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private float speed;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup canvasGroup;
    private bool isGameOver = false;

    public Ball ball;
    public Press press;

    private void Awake() 
    {
        retryButton.onClick.AddListener(() => InterstitialRetry());
        exitButton.onClick.AddListener(() => EndGame());
    }

    private void OnEnable() 
    {
        canvasGroup.DOFade(1,1);    
    }

    private void Update() 
    {
        if(slider.value <= 0 && !isGameOver)
        {
            EndGame();
            isGameOver = true;
        }
        else
        {
            slider.value -= speed * Time.deltaTime;
        }
    }

    
    public void InterstitialRetry()
    {
        
        ball.isRotate = true;
        ball.isRetry = true;
        ball.gameObject.SetActive(true);
        ball.transform.position = Vector3.zero;
        ball.rb.velocity = Vector2.zero;
        ball.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        gameObject.SetActive(false);
        press.transform.position = new Vector3(press.transform.position.x , 0, 0);

    }

    public void EndGame()
    {
        press.GameOverDirect();
        UiManager.Instance.GameOverUI();
        DataManager.Instance.UpdateBestScore();
        gameObject.SetActive(false);
    }
}