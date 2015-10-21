using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdControl : MonoBehaviour {
    // The ad to be shown when the game finishes
    private InterstitialAd interstitial;
    
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-7493941274287091/3779996767";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-7493941274287091/9096343563";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
    }
}
