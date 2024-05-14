using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoSingleton<AdManager>
{
    // 1.전면 광고 로드
    // 2.전면 광고 표시
    // 3.전면 광고 이벤트 듣기
    // 4.전면 광고 정리
    // 5.다음 전면 광고 미리 로드

    public bool isTest = true;
    private InterstitialAd _interstitialAd;

    protected override void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadInterstitialAd();
            // This callback is called once the MobileAds SDK is initialized.
        });
    }

    /// <summary>
    /// 1.전면 광고 로드
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        string _adUnitId = null;

#if UNITY_ANDROID
         _adUnitId = "ca-app-pub-1077488135922668/4441277383";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-1077488135922668/1255311033";
#else
         _adUnitId = "unexpected_platform";
#endif

        if (isTest)
            _adUnitId = "ca-app-pub-3940256099942544/4411468910";


        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }

    /// <summary>
    /// 2.전면 광고 표시
    /// </summary>
    public void ShowInterstitialAd(Action action = null)
    {
        LoadInterstitialAd();
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
            if(action != null)
            _interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                action();
            };

        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    /// <summary>
    /// 3.전면 광고 이벤트 듣기
    /// </summary>
    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            // 광고 재로드
            Debug.Log("Interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            // 광고 재로드 
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

}
