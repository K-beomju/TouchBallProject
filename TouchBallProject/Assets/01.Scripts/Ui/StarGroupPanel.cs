using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StarGroupPanel : MonoBehaviour
{
    private RectTransform rtm;
    [SerializeField] private RectTransform starRtm;
    [SerializeField] private Text starText;

    private void Awake() 
    {
        rtm = GetComponent<RectTransform>();
    }

    public void ShowStar(int star)
    {
        starText.text = star.ToString();
    }

    public void GetStarDirect()
    {
        starRtm.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360);
        DataManager.Instance.AddStar();
    }

    public void ChangeSkinPos(bool change)
    {
        if(change)
        rtm.anchoredPosition = new Vector2(0, -240);
        else
        rtm.anchoredPosition = Vector2.zero;
    }
}
