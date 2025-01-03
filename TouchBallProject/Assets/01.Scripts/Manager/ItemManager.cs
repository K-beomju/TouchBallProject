using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private GameObject itemObj;
    [SerializeField] private GameObject pressObj;
    [SerializeField] private GameObject ballObj;
    [SerializeField] private GameObject itemGroup;
    [SerializeField] private Text countText;

    private SpriteRenderer itemSr;
    private Press press;
    private Ball ball;
    [SerializeField] private Star star;
    private Camera mainCam;
    private WaitForSeconds delay = new WaitForSeconds(10);
    private float startTime;
    private float cooldown = 10;

    private int itemValue = 0;

    private List<System.Func<IEnumerator>> _itemPatterns = new List<System.Func<IEnumerator>>();

    public List<Star> starList = new List<Star>();
    public bool isSpawn = true;


    private void Awake()
    {
        mainCam = Camera.main;
        itemSr = itemObj.GetComponent<SpriteRenderer>();
        press = pressObj.GetComponent<Press>();
        ball = ballObj.GetComponent<Ball>();

        _itemPatterns.Add(BigPress);
        _itemPatterns.Add(GoldPress);
        _itemPatterns.Add(SpawnStar);
        Define.ShuffleList(_itemPatterns);

        _itemPatterns.Add(SlowBall);   // 처음 슬로우는 효과가 없어 뒤에 추가 

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
            yield return StartCoroutine(_itemPatterns[itemValue++]());
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

    private IEnumerator SpawnStar()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject starObj = Instantiate(star.gameObject,RandCameraViewPosition(0.2f , 0.8f), Quaternion.identity);
        }
        startTime = Time.time;
        while (Time.time - startTime < cooldown)
        {
            float remainingTime = cooldown - (Time.time - startTime);
            countText.text = remainingTime.ToString("F0") + "S";
            yield return null;
        }
    }



    public Vector2 RandCameraViewPosition(float x = 0.4f, float y = 0.7f)
    {
        return mainCam.ViewportToWorldPoint(new Vector3(Random.Range(x, y), Random.Range(x, y), 0));
    }


}
