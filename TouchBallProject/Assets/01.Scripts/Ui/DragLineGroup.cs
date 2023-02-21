using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DragLineGroup : MonoBehaviour
{
    public Image coolTimeImage;
    public Ball ball;

    public void ReloadDrag()
    {
        ball.isDragShot = true;
        coolTimeImage.fillAmount = 1;
        coolTimeImage.DOFillAmount(0, 3).OnComplete(() =>
        {
            ball.isDragShot = false;
        });
    }
}
