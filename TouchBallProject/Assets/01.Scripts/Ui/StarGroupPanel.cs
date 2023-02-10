using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarGroupPanel : MonoBehaviour
{
    [SerializeField] private RectTransform starRtm;
    [SerializeField] private Text starText;

    public void ShowStar(int star)
    {
        starText.text = star.ToString();
    }

    public void GetStarDirect()
    {
        starRtm.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360);
        DataManager.Instance.AddStar();
    }
}
