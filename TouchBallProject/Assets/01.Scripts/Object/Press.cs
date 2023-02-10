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

    [SerializeField] private SpriteRenderer ambient1Sr;
    [SerializeField] private SpriteRenderer ambient2Sr;
    [SerializeField] private Color goldAmbient1;
    [SerializeField] private Color goldAmbient2;
    private Color normalAmbient1;
    private Color normalAmbient2;
    #endregion


    public bool isGoldPress = false;
    public bool isChange = false;
    public GameObject goldParticle;

    private SpriteRenderer sr;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        Vector3 rightCenter = mainCam.ViewportToWorldPoint(new Vector3(1, 0.5f, mainCam.nearClipPlane));
        transform.position = rightCenter;
        sr = GetComponent<SpriteRenderer>();
        normalAmbient1 = ambient1Sr.color;
        normalAmbient2 = ambient2Sr.color;
    }

    public void ChangePressTransform()
    {
        isChange = true;
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
            transform.position = new Vector3(targetX + (transform.position.x > 0 ? -0.1f : 0.1f), Random.Range(-3.5f, 3.6f), 0);
            transform.DOMoveX(targetX, 0.1f).OnComplete(() => isChange = false);
        });
    }

    public void GoldPressItem()
    {

        isGoldPress = true;
        sr.DOColor(Color.yellow, 0.5f);
        ambient1Sr.color = goldAmbient1;
        ambient2Sr.color = goldAmbient2;
        goldParticle.SetActive(true);
    }

    public void ChangeNormalPress()
    {
        isGoldPress = false;
        sr.DOColor(Color.white, 0.5f);
        ambient1Sr.color = normalAmbient1;
        ambient2Sr.color = normalAmbient2;
        goldParticle.SetActive(false);

    }


    public void GameOverDirect()
    {
        transform.DOMoveX(transform.position.x + (transform.position.x > 0 ? 0.3f : -0.3f), 1f)
        .OnComplete(() => gameObject.SetActive(false));
    }

}
