using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private GameObject itemObj;
    private SpriteRenderer itemSr;

    [SerializeField] private GameObject pressObj;
    private Press press;
    [SerializeField] private GameObject ballObj;

    [SerializeField] private GameObject itemGroup;
    [SerializeField] private Text countText;
    
    private Camera mainCam;

    private WaitForSeconds delay = new WaitForSeconds(10);
    private float startTime;
    private float cooldown = 10;

    private List<System.Func<IEnumerator>> _itemPatterns = new List<System.Func<IEnumerator>>();



    public bool isSpawn = true;

    private void Awake()
    {
        mainCam = Camera.main;
        itemSr = itemObj.GetComponent<SpriteRenderer>();
        press = pressObj.GetComponent<Press>();

        _itemPatterns.Add(BigPress);
        _itemPatterns.Add(GoldPress);
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
            yield return  StartCoroutine(_itemPatterns[Random.Range(0, _itemPatterns.Count)]());
            itemGroup.SetActive(false);

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


    public Vector2 RandCameraViewPosition()
    {
        return mainCam.ViewportToWorldPoint(new Vector3(Random.Range(0.4f, 0.7f), Random.Range(0.4f, 0.7f), 0));
    }


}
