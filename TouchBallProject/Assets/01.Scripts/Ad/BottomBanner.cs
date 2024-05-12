using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BottomBanner : MonoBehaviour
{
    private BannerView bannerView;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1077488135922668/4845490116";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-1077488135922668/2241089640";
#else
            string adUnitId = "unexpected_platform";
#endif
        if (AdManager.Instance.isTest)
            adUnitId = "ca-app-pub-3940256099942544/6300978111";
    
        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
        AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // Create an empty ad request.
        //AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        //this.bannerView.LoadAd(request);
    }
}
