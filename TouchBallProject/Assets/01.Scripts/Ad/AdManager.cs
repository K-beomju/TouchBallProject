using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoSingleton<AdManager>
{
    public bool isTest = true;
    private InterstitialAd interstitial;
    public Action loadAction = () => { };

    protected override void Start()
    {
        MobileAds.Initialize(initStatus => RequestInterstitial());
    }

    // 전면
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1077488135922668/4441277383";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-1077488135922668/1255311033";
#else
        string adUnitId = "unexpected_platform";
#endif

        if (isTest)
            adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // this.interstitial = new InterstitialAd(adUnitId);
        // this.interstitial.OnAdClosed += HandleOnAdClosed;
        // AdRequest request = new AdRequest.Builder().Build();
        // this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        if (loadAction != null)
        {
            loadAction();
            loadAction = null;
        }
    }

    public void ShowInterstitialAd(Action action = null)
    {
        if (action != null)
            loadAction = action;

        ShowRewardedAd();
    }

    private void ShowRewardedAd()
    {
        // if (this.interstitial.IsLoaded())
        //     this.interstitial.Show();
    }
}
