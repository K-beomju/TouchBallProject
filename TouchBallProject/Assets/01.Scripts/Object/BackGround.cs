using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackGround : MonoBehaviour
{
    [SerializeField] private List<Color> colorList = new List<Color>();

    private SpriteRenderer sr;
    private int currentColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Define.ShuffleList(colorList);
    }

    public void ChangeBackColor()
    {
        sr.DOColor(colorList[currentColor++], 1);

        if (currentColor % colorList.Count == 0)
            currentColor = 0;
    }

    public void GameOver()
    {
        sr.DOColor(Color.white, 1);
    }


}
