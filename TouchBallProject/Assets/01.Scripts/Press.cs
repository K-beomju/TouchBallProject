using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Press : MonoBehaviour
{
    [SerializeField] private GameObject graySqure;
    [SerializeField] private GameObject lightGraySqure;
    private Sequence pressAmbientSq;
    private float pressIntensity;

    public void ChangePressTransform()
    {
        pressAmbientSq = DOTween.Sequence();
        pressIntensity = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            pressIntensity += 0.2f;
            pressAmbientSq.Join(transform.GetChild(i).transform.DOScaleY(1f + pressIntensity, 0.1f).SetLoops(2, LoopType.Yoyo));
        }
        pressAmbientSq.Play().OnComplete(() =>
        {

            float targetX = -transform.position.x;
            transform.position = new Vector3(targetX + (transform.position.x > 0 ? -0.5f : 0.5f), Random.Range(-4, 5), 0);
            transform.DOMoveX(targetX, 0.1f);


        });

    }

}
