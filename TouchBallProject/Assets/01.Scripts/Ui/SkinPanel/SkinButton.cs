using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinButton : MonoBehaviour
{
    public int cost;

    public Button button { get; set; }
    public Sprite sprite { get; set; }
    public Outline outline { get; set; }

    [SerializeField] private Image image;
    [SerializeField] private RectTransform cometRtm;
    [SerializeField] private CanvasGroup canvas;

    private void Awake()
    {
        sprite = image.sprite;  
        button = GetComponent<Button>();
        outline = GetComponent<Outline>();
    }

    public void BuySkinDirect()
    {
        if (IsCheck())
        {
            canvas.DOFade(0, 1);
            cometRtm.DOAnchorPosY(0, 1);
            cometRtm.DOSizeDelta(new Vector2(180, 180), 1);
        }
    }

    public void BuyInitSkin()
    {
        if (IsCheck())
        {
            canvas.gameObject.SetActive(false);
            cometRtm.anchoredPosition = new Vector2(0, 0);
            cometRtm.sizeDelta = new Vector2(150, 150);
        }
    }

    public void RotateDirect()
    {
        cometRtm.DORotate(new Vector3(0,0,-360), 5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental);

    }

    public bool IsCheck()
    {
        return canvas != null && cometRtm != null;
    }
}
