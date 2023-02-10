using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private GameObject itemObj;
    [SerializeField] private GameObject pressObj;
        public GameObject ballObj;
    [SerializeField] private GameObject itemGroup;
    [SerializeField] private Text countText;

    private SpriteRenderer itemSr;
    private Press press;
    private Ball ball;
    private Camera mainCam;

    private WaitForSeconds delay = new WaitForSeconds(10);
    private float startTime;
    private float cooldown = 10;

    private int itemValue = 0;

    private List<System.Func<IEnumerator>> _itemPatterns = new List<System.Func<IEnumerator>>();

    public bool isSpawn = true;


    private void Awake()
    {
        mainCam = Camera.main;
        itemSr = itemObj.GetComponent<SpriteRenderer>();
        press = pressObj.GetComponent<Press>();
        ball = ballObj.GetComponent<Ball>();

        _itemPatterns.Add(BigPress);
        _itemPatterns.Add(GoldPress);
        _itemPatterns.Add(SlowBall);

        Define.ShuffleList(_itemPatterns);

    }


    public void SpawnItem()
    {
        if (isSpawn)
        {
            itemSr.DOFade(0, 0);
            itemSr.DOFade(1, 1);
            itemObj.SetActive(true);
            itemObj.transform.position = RandCameraViewPosition();
            isSpawn = false;
        }
    }

    public void UseItem()
    {
        StartCoroutine(UseItemCo());


        IEnumerator UseItemCo()
        {
            itemGroup.SetActive(true);
            countText.text = "";
            yield return StartCoroutine(GoldPress());   //(_itemPatterns[itemValue++]());
            itemGroup.SetActive(false);
            if (itemValue >= _itemPatterns.Count)
            {
                Define.ShuffleList(_itemPatterns);
                itemValue = 0;
            }

            yield return delay;

            isSpawn = true;
        }

    }

    private IEnumerator BigPress()
    {
        press.transform.DOScaleY(3, 0.5f);
        startTime = Time.time;
        while (Time.time - startTime < cooldown)
        {
            float remainingTime = cooldown - (Time.time - startTime);
            countText.text = remainingTime.ToString("F0") + "S";
            yield return null;
        }
        press.transform.DOScaleY(1.5f, 0.5f);
    }

    private IEnumerator GoldPress()
    {
        press.GoldPressItem();
        startTime = Time.time;
        while (Time.time - startTime < cooldown)
        {
            float remainingTime = cooldown - (Time.time - startTime);
            countText.text = remainingTime.ToString("F0") + "S";
            yield return null;
        }
        press.ChangeNormalPress();
    }

    private IEnumerator SlowBall()
    {
        float currentSpeed = ball.moveSpeed;
        ball.moveSpeed = 1;
        ball.isSlow = true;
        startTime = Time.time;
        while (Time.time - startTime < cooldown)
        {
            float remainingTime = cooldown - (Time.time - startTime);
            countText.text = remainingTime.ToString("F0") + "S";
            yield return null;
        }
        ball.moveSpeed = currentSpeed;
        ball.isSlow = false;
    }



    public Vector2 RandCameraViewPosition()
    {
        return mainCam.ViewportToWorldPoint(new Vector3(Random.Range(0.4f, 0.7f), Random.Range(0.4f, 0.7f), 0));
    }


}
