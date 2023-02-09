using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject press;
    [SerializeField] private GameObject ball;

    private SpriteRenderer itemSr;
    private Camera mainCam;
    private WaitForSeconds delay = new WaitForSeconds(10);
    private float startTime;
    private float cooldown = 10;

    [SerializeField] private GameObject itemGroup;
    [SerializeField] private Text countText;

    public bool isSpawn = true;

    private void Awake()
    {
        mainCam = Camera.main;
        itemSr = item.GetComponent<SpriteRenderer>();
    }

    public void SpawnItem()
    {
        if (isSpawn)
        {
            itemSr.DOFade(0, 0);
            itemSr.DOFade(1, 1);
            item.SetActive(true);
            item.transform.position = RandCameraViewPosition();
            isSpawn = false;
        }
    }

    public void UseItem()
    {
        StartCoroutine(UseItemCo());


        IEnumerator UseItemCo()
        {
            // TODO
            itemGroup.SetActive(true);
            yield return StartCoroutine(BigPress());
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
        itemGroup.SetActive(false);
        press.transform.DOScaleY(1.5f, 0.5f);
    }


    public Vector2 RandCameraViewPosition()
    {
        return mainCam.ViewportToWorldPoint(new Vector3(Random.Range(0.4f, 0.7f), Random.Range(0.4f, 0.7f), 0));
    }


}
