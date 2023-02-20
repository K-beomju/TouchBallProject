using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private GameObject parentGroup;
    [SerializeField] private SpriteRenderer ballSr;
    [SerializeField] private List<SkinButton> skinBtnList = new List<SkinButton>();
    [SerializeField] private SkinPopup skinPopup;
    [SerializeField] private Button rewardStarButton;

    private const string SKIN_KEY_PREFIX = "Skin";
    private const string MOUNT_SKIN_KEY = "MountSkin";

    private void Start()
    {
        //ResetSkin();
        rewardStarButton.onClick.AddListener(() =>  AdManager.Instance.ShowInterstitialAd(() => GetRewardAd()));

        for (int i = 0; i < parentGroup.transform.childCount; i++)
        {
            int skinIndex = i;
            skinBtnList.Add(parentGroup.transform.GetChild(i).GetComponent<SkinButton>());
            skinBtnList[i].button.onClick.AddListener(() => BuySkin(skinIndex));

            string skinKey = GetSkinKey(skinIndex);
            if (!SecurityPlayerPrefs.HasKey(skinKey))
            {
                // 초기화 
                SecurityPlayerPrefs.SetBool(skinKey, false);
            }
            else
            {
                //초기화됐다면 구매했던 스킨들 체크 && 내가 현재 사용하고 있는 스킨 셋팅 
                if (SecurityPlayerPrefs.GetBool(skinKey, true))
                {
                    if (skinIndex >= 0 && skinIndex < skinBtnList.Count)
                        skinBtnList[skinIndex].BuyInitSkin();
                }
            }

        }

        if (!SecurityPlayerPrefs.HasKey(MOUNT_SKIN_KEY))
            SecurityPlayerPrefs.SetInt(MOUNT_SKIN_KEY, 0);

        SecurityPlayerPrefs.SetBool(GetSkinKey(0), true);

        MountSkin(SecurityPlayerPrefs.GetInt(MOUNT_SKIN_KEY, default));
    }


    public void BuySkin(int skinIndex)
    {
        string skinKey = GetSkinKey(skinIndex);
        if (SecurityPlayerPrefs.GetBool(skinKey, true))
        {
            MountSkin(skinIndex);
        }
        else
        {
            SoundManager.Instance.PlayButtonSound();
            skinPopup.gameObject.SetActive(true);

            skinPopup.buyAction += () =>
            {
                int cost = skinBtnList[skinIndex].cost;
                if (DataManager.Instance.Star >= cost)
                {
                    DataManager.Instance.MinusStar(skinBtnList[skinIndex].cost);
                    SecurityPlayerPrefs.SetBool(GetSkinKey(skinIndex), true);
                    skinBtnList[skinIndex].BuySkinDirect();
                    MountSkin(skinIndex);
                    skinPopup.gameObject.SetActive(false);

                    Debug.Log("Skin purchased");
                }
                else
                {
                    skinPopup.noStarText.gameObject.SetActive(true);
                    Debug.Log("Not enough stars");
                }
            };
        }
    }

    public void MountSkin(int temp)
    {
        skinBtnList.ForEach(x => x.outline.enabled = false);
        skinBtnList[temp].outline.enabled = true;
        ballSr.sprite = skinBtnList[temp].sprite;
        SecurityPlayerPrefs.SetInt(MOUNT_SKIN_KEY, temp);
    }


    private string GetSkinKey(int skinIndex)
    {
        return $"{skinIndex}{SKIN_KEY_PREFIX}";
    }

    public void ResetSkin()
    {
        for (int i = 1; i < parentGroup.transform.childCount; i++)
        {
            int skinIndex = i;
            string skinKey = GetSkinKey(skinIndex);
            SecurityPlayerPrefs.SetBool(skinKey, false);

        }
        SecurityPlayerPrefs.SetInt(MOUNT_SKIN_KEY, 0);

    }

    public void ShowDirect()
    {
        for (int i = 0; i < parentGroup.transform.childCount; i++)
        {
            skinBtnList[i].RotateDirect();
        }
    }

    public void StopDirect()
    {
        DOTween.KillAll();
    }

    public void GetRewardAd()
    {
        DataManager.Instance.AddStar(100);
        AdManager.Instance.RequestInterstitial();
    }


}
