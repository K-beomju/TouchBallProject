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
        ShuffleList(colorList);
    }

    public void ChangeBackColor()
    {
        sr.DOColor(colorList[currentColor++], 1);

        if (currentColor % colorList.Count == 0)
            currentColor = 0;
    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);

            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }

        return list;
    }

}
