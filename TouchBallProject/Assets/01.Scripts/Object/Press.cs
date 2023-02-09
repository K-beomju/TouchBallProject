using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class Press : MonoBehaviour
{
    #region 앰비언트 연출 
    private Sequence pressAmbientSq;
    private float pressIntensity;
    #endregion

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        Vector3 rightCenter = mainCam.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCam.nearClipPlane));
        transform.position = rightCenter;

    }

    public void ChangePressTransform()
    {
        pressAmbientSq = DOTween.Sequence();
        pressIntensity = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            pressIntensity += 0.17f;
            pressAmbientSq.Join(transform.GetChild(i).transform.DOScale(1f + pressIntensity, 0.1f).SetLoops(2, LoopType.Yoyo));
        }
        pressAmbientSq.Play().OnComplete(() =>
        {
            float targetX = -transform.localPosition.x;
            transform.position = new Vector3(targetX + (transform.position.x > 0 ? -0.3f : 0.3f), Random.Range(-3.5f, 3.6f), 0);
            transform.DOMoveX(targetX, 0.1f);
        });
    }

    public void GameOverDirect()
    {
        transform.DOMoveX(transform.position.x + (transform.position.x > 0 ? 0.3f : -0.3f), 1f);
    }

}
