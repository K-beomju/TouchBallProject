using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager : MonoSingleton<AdManager>
{
    private InterstitialAd interstitial;
    public Action action = () => {};

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1077488135922668/4067808726";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        if(action != null)
        {
            action();
            action = null;
        }
    }
    
    public void ShowInterstitial(Action _action)
    {
        RequestInterstitial();
        StartCoroutine(ShowInterstitial());
        action = _action;

        IEnumerator ShowInterstitial()
        {
            while(!this.interstitial.IsLoaded())
            {
                yield return new WaitForSeconds(0.2f);
            }
            this.interstitial.Show();
        }
        
    }
}
