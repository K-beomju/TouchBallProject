using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
    private Camera mainCam;
    private SpriteRenderer sr;

    private void Awake()
    {
        mainCam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ItemManager.Instance.starList.Add(this);
        sr.DOFade(1, 0);
        transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360);
        transform.DOScale(new Vector3(0.8f, 0.8f, 0), 0.5f).OnComplete(() => transform.DOScale(new Vector3(0.7f, 0.7f, 0), 0.5f));
    }

    public void GoStarPanel()
    {
        if(DOTween.IsTweening(this))
        {
            DOTween.Kill(this);
        }
        
        transform.DOPunchScale(new Vector3(0.8f, 0.8f, 0), 0.3f, 5, 1).OnComplete(() =>
        {
            transform.DOMove(new Vector3(1.7f, 4.6f, 0), 0.5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                UiManager.Instance.starGroup.GetStarDirect();
                ItemManager.Instance.starList.Remove(this);

            });
            sr.DOFade(0, 1);
        });
    }

    public void GameOverStar()
    {
        sr.DOFade(0,1);
    }
}
